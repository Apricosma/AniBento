using System.Text;
using System.Text.Json.Serialization;
using AniBento.Api.Data;
using AniBento.Api.Models.Auth;
using AniBento.Api.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

Console.WriteLine($"Connection: {builder.Configuration.GetConnectionString("DefaultConnection")}");

var env = builder.Environment;
var configuration = builder.Configuration;
var allowedOrigins = "_frontend";

// CORS configuration
if (env.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: allowedOrigins,
            policy =>
            {
                policy
                    .WithOrigins("http://localhost:5175", "http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }
        );
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            allowedOrigins,
            policy =>
            {
                policy
                    .WithOrigins("https://anibento.app", "https://www.anibento.app")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }
        );
    });
}

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Serialize enums as strings in JSON
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AniBento API", Version = "v1" });

    c.AddSecurityDefinition(
        "cookieAuth",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Cookie,
            Name = "anibento_auth",
            Description =
                "Authentication is managed via the HttpOnly 'anibento_auth' cookie. "
                + "Swagger UI cannot set or read this cookie via the 'Authorize' dialog. "
                + "To authenticate, first call the login endpoint so the browser receives the "
                + "Set-Cookie response; subsequent requests from Swagger UI will then include "
                + "the cookie automatically.",
        }
    );
});

// EF Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// ASP.NET Core Identity
builder
    .Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Cookie authentication for session management
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "anibento_auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = env.IsDevelopment()
        ? CookieSecurePolicy.SameAsRequest
        : CookieSecurePolicy.Always;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(14);

    // Overwrite asp.net default login redirects to status codes for API consumers
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization();

// Application services
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IUserMediaService, UserMediaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();
app.UseForwardedHeaders();

// Initialize and seed database in dev
// Going to be seeding prod while testing, update here to change that later
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (app.Environment.IsDevelopment())
    {
        // !!! DROP DATABASE !!!
        db.Database.EnsureDeleted();
        db.Database.Migrate();
        DbInitializer.Seed(db);
    }

    // if production
    if (!app.Environment.IsDevelopment())
    {
        db.Database.EnsureDeleted();
        db.Database.Migrate();
        DbInitializer.Seed(db);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowedOrigins);

//traefik handles https redirection
//if (!app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//}

app.Logger.LogInformation("Starting AniBento in {Environment}", app.Environment.EnvironmentName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using AniBento.Api.Data;
using AniBento.Api.Models;
using AniBento.Api.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace AniBento.Api.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // Role seeding
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole("User") { NormalizedName = "USER" },
                    new IdentityRole("Admin") { NormalizedName = "ADMIN" }
                );
            }

            // Skip if data already exists
            if (context.Medias.Any())
                return;

            context.Medias.AddRange(
                new Media
                {
                    Title = "Naruto",
                    Description = "A story about a young ninja.",
                    MediaType = "Anime",
                    ReleaseDate = new DateTime(2002, 10, 3),
                    Studio = "Studio Pierrot",
                    MediaImageUrl = "https://example.com/naruto.jpg",
                    EpisodeOrChapterCount = 220,
                    enteredAt = DateTime.UtcNow,
                },
                new Media
                {
                    Title = "One Piece",
                    Description = "A story about pirates searching for treasure.",
                    MediaType = "Anime",
                    ReleaseDate = new DateTime(1999, 10, 20),
                    Studio = "Toei Animation",
                    MediaImageUrl = "https://example.com/onepiece.jpg",
                    EpisodeOrChapterCount = 1000,
                    enteredAt = DateTime.UtcNow,
                },
                new Media
                {
                    Title = "Attack on Titan",
                    Description = "Humans fight against giant humanoid Titans.",
                    MediaType = "Anime",
                    ReleaseDate = new DateTime(2013, 4, 7),
                    Studio = "Wit Studio",
                    MediaImageUrl = "https://example.com/aot.jpg",
                    EpisodeOrChapterCount = 75,
                    enteredAt = DateTime.UtcNow,
                }
            );

            // User seeding
            if (!context.Users.Any())
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();

                var admin = new ApplicationUser
                {
                    UserName = "AdminSammy",
                    NormalizedUserName = "ADMINSAMMY",
                    Email = "adminsam@email.com",
                    NormalizedEmail = "ADMINSAM@EMAIL.COM",
                    EmailConfirmed = true,
                };

                var user = new ApplicationUser
                {
                    UserName = "TestUser",
                    NormalizedUserName = "TESTUSER",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                };

                context.Users.Add(admin);
                context.Users.Add(user);
                context.SaveChanges();

                admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123!");
                user.PasswordHash = passwordHasher.HashPassword(user, "User123!");

                context.Users.Update(admin);
                context.Users.Update(user);
                context.SaveChanges();

                var adminRole = context.Roles.Single(r => r.Name == "Admin");
                var userRole = context.Roles.Single(r => r.Name == "User");

                context.UserRoles.AddRange(
                    new IdentityUserRole<string> { UserId = admin.Id, RoleId = adminRole.Id },
                    new IdentityUserRole<string> { UserId = user.Id, RoleId = userRole.Id }
                );
            }

            // UserMedia library seeding
            if (!context.UserMedias.Any())
            {
                var adminUser = context.Users.Single(u => u.UserName == "AdminSammy");
                var testUser = context.Users.Single(u => u.UserName == "TestUser");
                var naruto = context.Medias.Single(m => m.Title == "Naruto");
                var onePiece = context.Medias.Single(m => m.Title == "One Piece");
                context.UserMedias.AddRange(
                    new UserMedia
                    {
                        UserId = adminUser.Id,
                        MediaId = naruto.Id,
                        Status = UserMediaStatus.Watching,
                        Rating = 5,
                        AddedAt = DateTime.UtcNow,
                    },
                    new UserMedia
                    {
                        UserId = adminUser.Id,
                        MediaId = onePiece.Id,
                        Status = UserMediaStatus.OnHold,
                        Rating = 4,
                        AddedAt = DateTime.UtcNow,
                    },
                    new UserMedia
                    {
                        UserId = testUser.Id,
                        MediaId = onePiece.Id,
                        Status = UserMediaStatus.Completed,
                        Rating = 4,
                        AddedAt = DateTime.UtcNow,
                    }
                );
            }

            context.SaveChanges();
        }
    }
}

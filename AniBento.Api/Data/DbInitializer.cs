using AniBento.Api.Data.DbSeedData;
using AniBento.Api.Models;
using AniBento.Api.Models.Auth;
using AniBento.Api.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace AniBento.Api.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // ---- Roles ----
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole("User") { NormalizedName = "USER" },
                    new IdentityRole("Admin") { NormalizedName = "ADMIN" }
                );
                context.SaveChanges();
            }

            // ---- Genres ----
            if (!context.Genres.Any())
            {
                context.Genres.AddRange(GenreSeed.CanonicalGenres);
                context.SaveChanges();
            }

            // ---- Media (+ Genres + Details) ----
            if (!context.Medias.Any())
            {
                // Load tracked genres from DB so EF links them in the join table
                var genresByNorm = context.Genres.ToDictionary(g => g.NameNormalized);

                var medias = DbMediaSeed.BuildMedias(genresByNorm);

                // optional safety normalization
                foreach (var media in medias)
                {
                    media.Title = media.Title.Trim();
                    media.Description = media.Description.Trim();
                    media.TitleNormalized = media.Title.ToUpperInvariant();
                    media.DescriptionNormalized = media.Description.ToUpperInvariant();
                }

                context.Medias.AddRange(medias);
                context.SaveChanges();
            }

            // ---- Users ----
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
                    ProfilePictureUrl =
                        "https://avatarfiles.alphacoders.com/375/thumb-1920-375160.jpeg",
                };

                var user = new ApplicationUser
                {
                    UserName = "TestUser",
                    NormalizedUserName = "TESTUSER",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                };

                // Hash BEFORE adding so you don't need Update() calls.
                admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123!");
                user.PasswordHash = passwordHasher.HashPassword(user, "User123!");

                context.Users.AddRange(admin, user);
                context.SaveChanges();

                var adminRole = context.Roles.Single(r => r.Name == "Admin");
                var userRole = context.Roles.Single(r => r.Name == "User");

                context.UserRoles.AddRange(
                    new IdentityUserRole<string> { UserId = admin.Id, RoleId = adminRole.Id },
                    new IdentityUserRole<string> { UserId = user.Id, RoleId = userRole.Id }
                );

                context.SaveChanges();
            }

            // ---- UserMedia ----
            if (!context.UserMedias.Any())
            {
                var adminUser = context.Users.Single(u => u.UserName == "AdminSammy");
                var testUser = context.Users.Single(u => u.UserName == "TestUser");

                // Titles are not guaranteed unique across types, so be explicit if you can.
                var naruto = context.Medias.Single(m =>
                    m.Title == "Naruto" && m.MediaType == MediaType.Manga
                );

                var onePiece = context.Medias.Single(m =>
                    m.Title == "One Piece" && m.MediaType == MediaType.Anime
                );

                context.UserMedias.AddRange(
                    new UserMedia
                    {
                        UserId = adminUser.Id,
                        MediaId = naruto.Id,
                        Status = UserMediaStatus.Reading,
                        Rating = 5,
                        AddedAt = DateTimeOffset.UtcNow,
                    },
                    new UserMedia
                    {
                        UserId = adminUser.Id,
                        MediaId = onePiece.Id,
                        Status = UserMediaStatus.OnHold,
                        Rating = 4,
                        AddedAt = DateTimeOffset.UtcNow,
                    },
                    new UserMedia
                    {
                        UserId = testUser.Id,
                        MediaId = onePiece.Id,
                        Status = UserMediaStatus.Completed,
                        Rating = 4,
                        AddedAt = DateTimeOffset.UtcNow,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}

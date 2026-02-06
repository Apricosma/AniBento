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

            // ---- Media + Details ----
            if (!context.Medias.Any())
            {
                context.Medias.AddRange(
                    new Media
                    {
                        Title = "Naruto",
                        Description = "A story about a young ninja.",
                        MediaType = MediaType.Manga,
                        ReleaseDate = new DateTime(2002, 10, 3),
                        MediaImageUrl = "https://example.com/naruto.jpg",
                        enteredAt = DateTime.UtcNow,
                        MangaDetails = new MangaDetails
                        {
                            Publisher = "Shueisha",
                            ChapterCount = 700,
                            VolumeCount = 72,
                            Genres = new[] { "Action", "Adventure", "Shounen" },
                        },
                    },
                    new Media
                    {
                        Title = "One Piece",
                        Description = "A story about pirates searching for treasure.",
                        MediaType = MediaType.Anime,
                        ReleaseDate = new DateTime(1999, 10, 20),
                        MediaImageUrl = "https://example.com/onepiece.jpg",
                        enteredAt = DateTime.UtcNow,
                        AnimeDetails = new AnimeDetails
                        {
                            Studio = "Toei Animation",
                            EpisodeCount = 1000,
                            Genres = new[] { "Action", "Adventure", "Shounen" },
                        },
                    },
                    new Media
                    {
                        Title = "Attack on Titan",
                        Description = "Humans fight against giant humanoid Titans.",
                        MediaType = MediaType.Anime,
                        ReleaseDate = new DateTime(2013, 4, 7),
                        MediaImageUrl = "https://example.com/aot.jpg",
                        enteredAt = DateTime.UtcNow,
                        AnimeDetails = new AnimeDetails
                        {
                            Studio = "Wit Studio",
                            EpisodeCount = 75,
                            Genres = new[] { "Action", "Drama", "Dark Fantasy" },
                        },
                    },
                    new Media
                    {
                        Title = "Spirited Away",
                        Description = "A girl enters a spirit world and must find her way back.",
                        MediaType = MediaType.Movie,
                        ReleaseDate = new DateTime(2001, 7, 20),
                        MediaImageUrl = "https://example.com/spiritedaway.jpg",
                        enteredAt = DateTime.UtcNow,
                        MovieDetails = new MovieDetails
                        {
                            Studio = "Studio Ghibli",
                            Directors = new[] { "Hayao Miyazaki" },
                            Genres = new[] { "Fantasy", "Adventure" },
                        },
                    }
                );

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

                // Create users first (so they have IDs), then set password hashes
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

                context.SaveChanges();
            }

            // ---- UserMedia ----
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
                        Status = Models.Enums.UserMediaStatus.Reading, // Manga
                        Rating = 5,
                        AddedAt = DateTime.UtcNow,
                    },
                    new UserMedia
                    {
                        UserId = adminUser.Id,
                        MediaId = onePiece.Id,
                        Status = Models.Enums.UserMediaStatus.OnHold,
                        Rating = 4,
                        AddedAt = DateTime.UtcNow,
                    },
                    new UserMedia
                    {
                        UserId = testUser.Id,
                        MediaId = onePiece.Id,
                        Status = Models.Enums.UserMediaStatus.Completed,
                        Rating = 4,
                        AddedAt = DateTime.UtcNow,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}

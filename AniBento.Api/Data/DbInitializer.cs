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

            // ---- Media + Details ----
            if (!context.Medias.Any())
            {
                context.Medias.AddRange(DbMediaSeed.Medias);

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
                        Status = Models.Enums.UserMediaStatus.Reading,
                        Rating = 5,
                        AddedAt = DateTimeOffset.UtcNow,
                    },
                    new UserMedia
                    {
                        UserId = adminUser.Id,
                        MediaId = onePiece.Id,
                        Status = Models.Enums.UserMediaStatus.OnHold,
                        Rating = 4,
                        AddedAt = DateTimeOffset.UtcNow,
                    },
                    new UserMedia
                    {
                        UserId = testUser.Id,
                        MediaId = onePiece.Id,
                        Status = Models.Enums.UserMediaStatus.Completed,
                        Rating = 4,
                        AddedAt = DateTimeOffset.UtcNow,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}

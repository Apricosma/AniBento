using AniBento.Api.Models;
using AniBento.Api.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AniBento.Api.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Media> Medias => Set<Media>();
        public DbSet<UserMedia> UserMedias => Set<UserMedia>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var utcConverter = new ValueConverter<DateTime, DateTime>(
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

            modelBuilder.Entity<Media>().Property(m => m.ReleaseDate).HasConversion(utcConverter);
            modelBuilder.Entity<Media>().Property(m => m.enteredAt).HasConversion(utcConverter);

            modelBuilder.Entity<UserMedia>(entity =>
            {
                entity.HasKey(um => new { um.UserId, um.MediaId });

                entity.HasOne(um => um.User).WithMany().HasForeignKey(um => um.UserId);

                entity
                    .HasOne(um => um.Media)
                    .WithMany(m => m.UserMedias)
                    .HasForeignKey(um => um.MediaId);

                entity.HasIndex(um => um.UserId);
            });
        }
    }
}

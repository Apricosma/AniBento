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
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<UserMedia> UserMedias => Set<UserMedia>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var utcConverter = new ValueConverter<DateTime, DateTime>(
            //    v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
            //    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            //);

            modelBuilder.Entity<Media>().Property(m => m.ReleaseDate).HasColumnType("date");
            modelBuilder
                .Entity<Media>()
                .Property(m => m.EnteredAt)
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("now()");
            modelBuilder.Entity<Media>().Property(m => m.MediaType).HasConversion<string>();

            modelBuilder.Entity<AnimeDetails>().HasKey(ad => ad.MediaId);
            modelBuilder.Entity<MangaDetails>().HasKey(md => md.MediaId);
            modelBuilder.Entity<MovieDetails>().HasKey(md => md.MediaId);

            modelBuilder
                .Entity<Media>()
                .HasOne(m => m.AnimeDetails)
                .WithOne(d => d.Media)
                .HasForeignKey<AnimeDetails>(d => d.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Media>()
                .HasOne(m => m.MangaDetails)
                .WithOne(d => d.Media)
                .HasForeignKey<MangaDetails>(d => d.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Media>()
                .HasOne(m => m.MovieDetails)
                .WithOne(d => d.Media)
                .HasForeignKey<MovieDetails>(d => d.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieDetails>().Property(m => m.Directors).HasColumnType("text[]");

            modelBuilder.Entity<Genre>().HasIndex(g => g.NameNormalized).IsUnique();

            modelBuilder.Entity<MediaGenre>(entity =>
            {
                entity.HasKey(x => new { x.MediaId, x.GenreId });

                entity
                    .HasOne(x => x.Media)
                    .WithMany(m => m.MediaGenres)
                    .HasForeignKey(x => x.MediaId);

                entity
                    .HasOne(x => x.Genre)
                    .WithMany(g => g.MediaGenres)
                    .HasForeignKey(x => x.GenreId);

                entity.HasIndex(x => x.GenreId);
                entity.HasIndex(x => x.MediaId);
            });

            modelBuilder.Entity<UserMedia>(entity =>
            {
                entity.HasKey(um => new { um.UserId, um.MediaId });

                entity.HasOne(um => um.User).WithMany().HasForeignKey(um => um.UserId);

                entity
                    .HasOne(um => um.Media)
                    .WithMany(m => m.UserMedias)
                    .HasForeignKey(um => um.MediaId);

                entity.Property(um => um.Status).HasConversion<string>();

                entity.HasIndex(um => um.UserId);
            });
        }
    }
}

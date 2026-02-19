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
        public DbSet<MediaCollection> Collections => Set<MediaCollection>();
        public DbSet<CollectionItem> CollectionItems => Set<CollectionItem>();

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
                entity.HasKey(um => um.Id);

                entity.HasIndex(um => um.UserId);
                entity.HasIndex(um => new { um.UserId, um.MediaId }).IsUnique();

                entity
                    .HasOne(um => um.User)
                    .WithMany(u => u.UserMedias)
                    .HasForeignKey(um => um.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                    .HasOne(um => um.Media)
                    .WithMany(m => m.UserMedias)
                    .HasForeignKey(um => um.MediaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(um => um.Status).HasConversion<string>();
            });

            modelBuilder.Entity<MediaCollection>(entity =>
            {
                entity.HasKey(mc => mc.Id);

                entity.Property(mc => mc.Name).IsRequired();

                entity.HasIndex(mc => mc.UserId);

                entity
                    .HasOne(mc => mc.User)
                    .WithMany(u => u.Collections)
                    .HasForeignKey(mc => mc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CollectionItem>(entity =>
            {
                entity.HasKey(ci => ci.Id);

                entity.HasIndex(ci => ci.CollectionId);
                entity.HasIndex(ci => ci.UserMediaId);
                entity.HasIndex(ci => new { ci.CollectionId, ci.UserMediaId }).IsUnique();

                entity
                    .HasOne(ci => ci.Collection)
                    .WithMany(c => c.CollectionItems)
                    .HasForeignKey(ci => ci.CollectionId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity
                    .HasOne(ci => ci.UserMedia)
                    .WithMany(um => um.CollectionItems)
                    .HasForeignKey(ci => ci.UserMediaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

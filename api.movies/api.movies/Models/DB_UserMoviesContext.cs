using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace api.movies.Models
{
    public partial class DB_UserMoviesContext : DbContext
    {
        public DB_UserMoviesContext()
        {
        }

        public DB_UserMoviesContext(DbContextOptions<DB_UserMoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieReview> MovieReviews { get; set; }
        public virtual DbSet<UserAkun> UserAkuns { get; set; }
        public virtual DbSet<UserMovie> UserMovies { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public IConfiguration Configuration { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Judul)
                    .HasMaxLength(255)
                    .HasColumnName("judul");

                entity.Property(e => e.TahunRilis).HasColumnName("tahun_rilis");
            });

            modelBuilder.Entity<MovieReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK__MovieRev__60883D90C4007C14");

                entity.ToTable("MovieReview");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.Komentar).HasColumnName("komentar");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.TanggalReview)
                    .HasColumnType("datetime")
                    .HasColumnName("tanggal_review");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieReviews)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__MovieRevi__movie__534D60F1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MovieReviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MovieRevi__user___5441852A");
            });

            modelBuilder.Entity<UserAkun>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserAkun__B9BE370FD1FC2E74");

                entity.ToTable("UserAkun");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<UserMovie>(entity =>
            {
                entity.HasKey(e => e.UserMovieID)
                    .HasName("PK__UserMovie__B9BE370FD1FC2E75");

                entity.ToTable("UserMovie");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__UserMovie__movie__5070F446");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserMovie__user___4F7CD00D");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId)
                    .HasName("PK__UserProf__AEBB701F98AD16D8");

                entity.ToTable("UserProfile");

                entity.Property(e => e.ProfileId).HasColumnName("profile_id");

                entity.Property(e => e.Alamat)
                    .HasMaxLength(255)
                    .HasColumnName("alamat");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Nama)
                    .HasMaxLength(255)
                    .HasColumnName("nama");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserProfi__user___4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

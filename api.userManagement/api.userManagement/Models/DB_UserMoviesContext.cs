using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace api.userManagement.Models
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

        public virtual DbSet<UserAkun> UserAkuns { get; set; }
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

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TournamentsIS.Models
{
    public partial class TournamentDBContext : DbContext
    {
        public TournamentDBContext()
        {
        }

        public TournamentDBContext(DbContextOptions<TournamentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clubs> Clubs { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Tournaments> Tournaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server= DESKTOP-5AOTR9Q; Database= TournamentDB;Trusted_Connection=True;Integrated security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clubs>(entity =>
            {
                entity.HasKey(e => e.ClubId);

                entity.Property(e => e.ClubAddress)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ClubDesc)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ClubName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clubs_Groups");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clubs_Tournaments");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK_Gruops");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Tournaments>(entity =>
            {
                entity.HasKey(e => e.TournamentId);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TournamentLocation)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.TournamentName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Slumming.Models
{
    public partial class SlummingContext : DbContext
    {
        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Deal> Deal { get; set; }
        public virtual DbSet<Salesman> Salesman { get; set; }
        public virtual DbSet<State> State { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {          
            optionsBuilder.UseSqlServer(@"data source=.\bearon2017;initial catalog=Slumming;user id=sa;password=sNET135!;MultipleActiveResultSets=True;App=EntityFramework");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.Property(e => e.Appartment)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OwnerName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Apartment)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Apartment_City");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Apartment)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Apartment_Client");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Apartment)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Apartment_State");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.BillingAdsress).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Deal>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.RegisterId).HasMaxLength(50);

                entity.HasOne(d => d.Appartment)
                    .WithMany(p => p.Deal)
                    .HasForeignKey(d => d.AppartmentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Deal_Apartment");

                entity.HasOne(d => d.Salesman)
                    .WithMany(p => p.Deal)
                    .HasForeignKey(d => d.SalesmanId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Deal_Salesman");
            });

            modelBuilder.Entity<Salesman>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
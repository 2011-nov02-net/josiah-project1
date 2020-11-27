using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class StoreAppDbContext : DbContext
    {
        public StoreAppDbContext(DbContextOptions<StoreAppDbContext> options) : base(options) { }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationEntity>(entity =>
            {
                entity.ToTable("Locations");

                entity.Property(e => e.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<LocationEntity>()
                .HasMany<ProductEntity>(e => e.Inventory);

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.ToTable("Customers");

                entity.Property(e => e.FirstName)
                    .IsRequired();
                entity.Property(e => e.LastName)
                    .IsRequired();

            });

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.ToTable("Products");

                entity.Property(e => e.Name)
                    .IsRequired();
                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("decimal");
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("Orders");

                entity.Property(e => e.Time)
                    .IsRequired();

                entity.Navigation(e => e.Customer)
                    .IsRequired();

                entity.Navigation(e => e.Location)
                    .IsRequired();
            });

            modelBuilder.Entity<OrderEntity>()
                .HasMany<ProductEntity>(e => e.Items);
        }
    }
}

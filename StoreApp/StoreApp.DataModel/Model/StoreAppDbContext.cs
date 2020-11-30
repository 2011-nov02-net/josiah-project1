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
        public DbSet<OrderItemsEntity> OrderItems { get; set; }
        public DbSet<InventoryItemsEntity> InventoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationEntity>(entity =>
            {
                entity.ToTable("Locations");

                entity.Property(e => e.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.ToTable("Customers");

                entity.Property(e => e.FirstName)
                    .IsRequired();
                entity.Property(e => e.LastName)
                    .IsRequired();
                entity.Property(e => e.Email)
                    .IsRequired();

            });

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.ToTable("Products");

                entity.HasMany(e => e.InventoryItems)
                    .WithOne(l => l.Product);

                entity.HasMany(e => e.OrderItems)
                    .WithOne(o => o.Product);

                entity.Property(e => e.Name)
                    .IsRequired();
                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("decimal(16,2)");
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("Orders");

                entity.Property(e => e.Time)
                    .IsRequired();

                entity.HasOne(e => e.Customer)
                    .WithMany(o => o.Orders)
                    .HasForeignKey(k => k.CustomerId)
                    .HasConstraintName("FK_Customer_Order")
                    .IsRequired();
                    
                entity.HasOne(e => e.Location)
                    .WithMany(o => o.Orders)
                    .HasForeignKey(k => k.LocationId)
                    .HasConstraintName("FK_Location_Order")
                    .IsRequired();
            });

            modelBuilder.Entity<InventoryItemsEntity>(entity =>
            {
                entity.ToTable("InventoryItems");

                entity.HasOne(e => e.Location)
                    .WithMany(d => d.Inventory)
                    .HasForeignKey(k => k.LocationId)
                    .HasConstraintName("FK_Location_Item");
            });

            modelBuilder.Entity<OrderItemsEntity>(entity =>
            {
                entity.ToTable("OrderItems");

                entity.HasOne(e => e.Order)
                    .WithMany(d => d.Items)
                    .HasForeignKey(k => k.OrderId)
                    .HasConstraintName("FK_Order_Item");
            });

        }
    }
}

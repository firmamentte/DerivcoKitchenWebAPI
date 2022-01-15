using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DerivcoKitchenWebAPI.Data.Entities
{
    public partial class DerivcoKitchenContext : DbContext
    {
        public DerivcoKitchenContext()
        {
        }

        public DerivcoKitchenContext(DbContextOptions<DerivcoKitchenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessToken> AccessTokens { get; set; } = null!;
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<MenuCategory> MenuCategories { get; set; } = null!;
        public virtual DbSet<MenuItem> MenuItems { get; set; } = null!;
        public virtual DbSet<MenuItemPurchaseOrder> MenuItemPurchaseOrders { get; set; } = null!;
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(StaticClass.DatabaseHelper.ConnectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessToken>(entity =>
            {
                entity.ToTable("AccessToken");

                entity.Property(e => e.AccessTokenId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccessTokenValue)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("ApplicationUser");

                entity.Property(e => e.ApplicationUserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuCategory>(entity =>
            {
                entity.ToTable("MenuCategory");

                entity.Property(e => e.MenuCategoryId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("MenuItem");

                entity.Property(e => e.MenuItemId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PictureFileName)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MenuCategory)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.MenuCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuItem_MenuCategory");
            });

            modelBuilder.Entity<MenuItemPurchaseOrder>(entity =>
            {
                entity.ToTable("MenuItemPurchaseOrder");

                entity.Property(e => e.MenuItemPurchaseOrderId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.PictureFileName)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MenuItem)
                    .WithMany(p => p.MenuItemPurchaseOrders)
                    .HasForeignKey(d => d.MenuItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuItemPurchaseOrder_MenuItem");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.MenuItemPurchaseOrders)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuItemPurchaseOrder_PurchaseOrder");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("PurchaseOrder");

                entity.Property(e => e.PurchaseOrderId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseOrderNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrder_ApplicationUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

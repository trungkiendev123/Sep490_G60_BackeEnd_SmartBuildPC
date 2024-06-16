using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class SMARTPCContext : DbContext
    {
        public SMARTPCContext()
        {
        }

        public SMARTPCContext(DbContextOptions<SMARTPCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<FilterString> FilterStrings { get; set; } = null!;
        public virtual DbSet<FilterType> FilterTypes { get; set; } = null!;
        public virtual DbSet<FilterTypeCategory> FilterTypeCategories { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Pcbuild> Pcbuilds { get; set; } = null!;
        public virtual DbSet<PcbuildPart> PcbuildParts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductStore> ProductStores { get; set; } = null!;
        public virtual DbSet<ProductWarranty> ProductWarranties { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=SMARTPC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.Username, "UQ__Account__536C85E464BFAA45")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Account__A9D10534B0F23F3E")
                    .IsUnique();

                entity.Property(e => e.AccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("AccountID");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Expired)
                    .HasColumnType("datetime")
                    .HasColumnName("expired");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RefreshToken).HasColumnType("text");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.BillDate).HasColumnType("date");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.TaxIn).HasColumnName("TaxIN");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Bill__OrderID__59063A47");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Cart");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Customer)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Cart__CustomerID__00200768");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Cart__ProductID__5AEE82B9");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.CategoryName, "UQ__Category__8517B2E09FD8F8A7")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.CommentDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ReplyStaffId).HasColumnName("ReplyStaffID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__Custome__0A9D95DB");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__Product__0B91BA14");

                entity.HasOne(d => d.ReplyStaff)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ReplyStaffId)
                    .HasConstraintName("FK__Comment__ReplySt__02084FDA");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Phone, "UQ__Customer__5C7E359EC8E057B8")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Account");
            });

            modelBuilder.Entity<FilterString>(entity =>
            {
                entity.ToTable("FilterString");

                entity.Property(e => e.FilterStringId).HasColumnName("FilterStringID");

                entity.Property(e => e.FilterString1)
                    .HasColumnType("text")
                    .HasColumnName("FilterString");

                entity.Property(e => e.FilterTypeCategoryId).HasColumnName("FilterTypeCategoryID");

                entity.HasOne(d => d.FilterTypeCategory)
                    .WithMany(p => p.FilterStrings)
                    .HasForeignKey(d => d.FilterTypeCategoryId)
                    .HasConstraintName("FK_FilterString_FilterTypeCategory");
            });

            modelBuilder.Entity<FilterType>(entity =>
            {
                entity.ToTable("FilterType");

                entity.Property(e => e.FilterTypeId).HasColumnName("FilterTypeID");

                entity.Property(e => e.FilterType1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FilterType");
            });

            modelBuilder.Entity<FilterTypeCategory>(entity =>
            {
                entity.ToTable("FilterTypeCategory");

                entity.Property(e => e.FilterTypeCategoryId).HasColumnName("FilterTypeCategoryID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.FilterTypeId).HasColumnName("FilterTypeID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FilterTypeCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_FilterTypeCategory_Category1");

                entity.HasOne(d => d.FilterType)
                    .WithMany(p => p.FilterTypeCategories)
                    .HasForeignKey(d => d.FilterTypeId)
                    .HasConstraintName("FK_FilterTypeCategory_Category");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.PcbuildId).HasColumnName("PCBuildID");

                entity.Property(e => e.Pctype)
                    .HasMaxLength(255)
                    .HasColumnName("PCType");

                entity.HasOne(d => d.Pcbuild)
                    .WithMany()
                    .HasForeignKey(d => d.PcbuildId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_PCBuild");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.OrderAddress).HasMaxLength(255);

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveDate).HasColumnType("date");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Order__CustomerI__05D8E0BE");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PricePerItem).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.WarrantyId).HasColumnName("WarrantyID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Order__60A75C0F");

                entity.HasOne(d => d.Warranty)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.WarrantyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Warra__619B8048");
            });

            modelBuilder.Entity<Pcbuild>(entity =>
            {
                entity.ToTable("PCBuild");

                entity.Property(e => e.PcbuildId).HasColumnName("PCBuildID");

                entity.Property(e => e.BuildDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Pcbuilds)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__PCBuild__Custome__09A971A2");
            });

            modelBuilder.Entity<PcbuildPart>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PCBuildParts");

                entity.Property(e => e.PcbuildId).HasColumnName("PCBuildID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Pcbuild)
                    .WithMany()
                    .HasForeignKey(d => d.PcbuildId)
                    .HasConstraintName("FK__PCBuildPa__PCBui__6477ECF3");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__PCBuildPa__PartI__6383C8BA");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.ImageLink).HasColumnType("text");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Tag).HasColumnType("text");

                entity.Property(e => e.Tdp).HasColumnName("TDP");

                entity.Property(e => e.Warranty)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Product__Categor__656C112C");
            });

            modelBuilder.Entity<ProductStore>(entity =>
            {
                entity.ToTable("ProductStore");

                entity.Property(e => e.ProductStoreId).HasColumnName("ProductStoreID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductStores)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSt__Produ__66603565");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProductStores)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSt__Store__08B54D69");
            });

            modelBuilder.Entity<ProductWarranty>(entity =>
            {
                entity.HasKey(e => e.WarrantyId)
                    .HasName("PK__ProductW__2ED318F3315F1B7E");

                entity.ToTable("ProductWarranty");

                entity.Property(e => e.WarrantyId).HasColumnName("WarrantyID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.WarrantyReceive).HasColumnType("date");

                entity.Property(e => e.WarrantySentDate).HasColumnType("date");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductWarranties)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductWa__Produ__68487DD7");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.StoreName).HasMaxLength(100);
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffId)
                    .ValueGeneratedNever()
                    .HasColumnName("StaffID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Staff_Account");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Staff__StoreID__7D439ABD");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

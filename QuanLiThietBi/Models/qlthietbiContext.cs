using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuanLiThietBi.Models
{
    public partial class qlthietbiContext : DbContext
    {
        public qlthietbiContext()
        {
        }

        public qlthietbiContext(DbContextOptions<qlthietbiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBorrowing> TblBorrowings { get; set; } = null!;
        public virtual DbSet<TblCategory> TblCategories { get; set; } = null!;
        public virtual DbSet<TblComponent> TblComponents { get; set; } = null!;
        public virtual DbSet<TblEmployee> TblEmployees { get; set; } = null!;
        public virtual DbSet<TblLocation> TblLocations { get; set; } = null!;
        public virtual DbSet<TblOrder> TblOrders { get; set; } = null!;
        public virtual DbSet<TblProduct> TblProducts { get; set; } = null!;
        public virtual DbSet<TblRole> TblRoles { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\;Database=qlthietbi;Trusted_Connection=true;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBorrowing>(entity =>
            {
                entity.HasKey(e => e.BorrowingId)
                    .HasName("PK__tbl_borr__37A8ECDCA8C746E5");

                entity.ToTable("tbl_borrowings");

                entity.Property(e => e.BorrowingId).HasColumnName("borrowing_id");

                entity.Property(e => e.BorrowDate)
                    .HasColumnType("datetime")
                    .HasColumnName("borrow_date");

                entity.Property(e => e.ProductId).HasColumnName("product_ID");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("datetime")
                    .HasColumnName("return_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblBorrowings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_borro__produ__3B75D760");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblBorrowings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_borro__user___3A81B327");
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__tbl_cate__D54EE9B40F4E7F06");

                entity.ToTable("tbl_categories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.NameCategory)
                    .HasMaxLength(255)
                    .HasColumnName("name_category");
            });

            modelBuilder.Entity<TblComponent>(entity =>
            {
                entity.HasKey(e => e.ComponentId)
                    .HasName("PK__tbl_comp__AEB1DA59FE312448");

                entity.ToTable("tbl_components");

                entity.Property(e => e.ComponentId).HasColumnName("component_id");

                entity.Property(e => e.Dvt)
                    .HasMaxLength(255)
                    .HasColumnName("DVT");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("manufacturer");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ProductId).HasColumnName("product_ID");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("serial_number");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblComponents)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_compo__produ__33D4B598");
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__tbl_empl__C52E0BA8A6DCD984");

                entity.ToTable("tbl_employees");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .HasColumnName("phone_number");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TblEmployees)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_emplo__locat__2A4B4B5E");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblEmployees)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_emplo__role___2B3F6F97");
            });

            modelBuilder.Entity<TblLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__tbl_loca__771831EA96192A76");

                entity.ToTable("tbl_locations");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.NameLocation)
                    .HasMaxLength(255)
                    .HasColumnName("name_location");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__tbl_orde__4659622992FA30AF");

                entity.ToTable("tbl_orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.ProductId).HasColumnName("product_ID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_order__produ__37A5467C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_order__user___36B12243");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__tbl_prod__47027DF5EA2596FB");

                entity.ToTable("tbl_products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_ID");

                entity.Property(e => e.LocationId).HasColumnName("location_ID");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("manufacturer");

                entity.Property(e => e.Model)
                    .HasMaxLength(255)
                    .HasColumnName("model");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("purchase_date");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("serial_number");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.WarrantyEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("warranty_end_date");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_produ__categ__300424B4");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__tbl_produ__locat__30F848ED");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__tbl_role__760965CC5D61F636");

                entity.ToTable("tbl_roles");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(255)
                    .HasColumnName("name_role");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tbl_user__B9BE370FC7126E90");

                entity.ToTable("tbl_users");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

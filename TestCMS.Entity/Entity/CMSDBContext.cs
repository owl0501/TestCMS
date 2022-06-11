using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestCMS.Entity.Entity
{
    public partial class CMSDBContext : DbContext
    {
        public CMSDBContext()
        {
        }

        public CMSDBContext(DbContextOptions<CMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartTable> CartTable { get; set; }
        public virtual DbSet<CategoryTable> CategoryTable { get; set; }
        public virtual DbSet<ProductTable> ProductTable { get; set; }
        public virtual DbSet<ShippingTable> ShippingTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CMSDB;User ID=sa;Password=Pw0922435545;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<CartTable>(entity =>
            {
                entity.ToTable("CartTable");

                entity.Property(e => e.ShipStatus)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasDefaultValueSql("('no')")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartTables)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CartTable__Produ__5AEE82B9");
            });

            modelBuilder.Entity<CategoryTable>(entity =>
            {
                entity.ToTable("CategoryTable");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ProductTable>(entity =>
            {
                entity.ToTable("ProductTable");

                entity.Property(e => e.Image).HasMaxLength(300);

                entity.Property(e => e.Intro).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ReleaseDatetime).HasColumnType("datetime");

                entity.Property(e => e.SupplyStatus)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductTables)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductTa__Categ__534D60F1");
            });

            modelBuilder.Entity<ShippingTable>(entity =>
            {
                entity.ToTable("ShippingTable");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ShipId)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("ShipID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ShippingTables)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShippingT__Categ__59FA5E80");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

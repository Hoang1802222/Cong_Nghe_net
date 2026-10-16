using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace nhom18.Models;

public partial class QuanLyQuanCafe1Context : DbContext
{
    public QuanLyQuanCafe1Context()
    {
    }

    public QuanLyQuanCafe1Context(DbContextOptions<QuanLyQuanCafe1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Ban> Bans { get; set; }

    public virtual DbSet<ChiTietOrder> ChiTietOrders { get; set; }

    public virtual DbSet<DanhMucMon> DanhMucMons { get; set; }

    public virtual DbSet<Mon> Mons { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress01;Database=QuanLyQuanCafe1;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ban>(entity =>
        {
            entity.HasKey(e => e.MaBan).HasName("PK__Ban__3520ED6C239DC8DA");

            entity.ToTable("Ban");

            entity.Property(e => e.KhuVuc).HasMaxLength(100);
            entity.Property(e => e.TenBan).HasMaxLength(50);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("Trống");
        });

        modelBuilder.Entity<ChiTietOrder>(entity =>
        {
            entity.HasKey(e => new { e.MaOrder, e.MaMon });

            entity.ToTable("ChiTietOrder");

            entity.Property(e => e.DonGia).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.GhiChu).HasMaxLength(500);

            entity.HasOne(d => d.MaMonNavigation).WithMany(p => p.ChiTietOrders)
                .HasForeignKey(d => d.MaMon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietOrder_Mon");

            entity.HasOne(d => d.MaOrderNavigation).WithMany(p => p.ChiTietOrders)
                .HasForeignKey(d => d.MaOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietOrder_Order");
        });

        modelBuilder.Entity<DanhMucMon>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DanhMucM__2725866E1DB56522");

            entity.ToTable("DanhMucMon");

            entity.Property(e => e.MaDm).HasColumnName("MaDM");
            entity.Property(e => e.TenDm)
                .HasMaxLength(100)
                .HasColumnName("TenDM");
        });

        modelBuilder.Entity<Mon>(entity =>
        {
            entity.HasKey(e => e.MaMon).HasName("PK__Mon__3A5B29A82E5F8F17");

            entity.ToTable("Mon");

            entity.Property(e => e.AnhMon).HasMaxLength(500);
            entity.Property(e => e.GiaBan).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.MaDm).HasColumnName("MaDM");
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.TenMon).HasMaxLength(150);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(10)
                .HasDefaultValue("Còn");

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.Mons)
                .HasForeignKey(d => d.MaDm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mon_DanhMucMon");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.MaOrder).HasName("PK__Order__50559EF7125BB0C9");

            entity.ToTable("Order");

            entity.Property(e => e.GioVao).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.LoaiOrder).HasMaxLength(20);
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.TongTien).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Chưa thanh toán");

            entity.HasOne(d => d.MaBanNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MaBan)
                .HasConstraintName("FK_Order_Ban");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TaiKhoan__272500704C27B0A1");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TaiKhoan__55F68FC06C02FB6C").IsUnique();

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MatKhauHash).HasMaxLength(255);
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VaiTro).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

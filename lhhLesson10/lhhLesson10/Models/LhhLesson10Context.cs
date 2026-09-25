using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lhhLesson10.Models;

public partial class LhhLesson10Context : DbContext
{
    public LhhLesson10Context()
    {
    }

    public LhhLesson10Context(DbContextOptions<LhhLesson10Context> options)
        : base(options)
    {
    }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS01;Database=lhhLesson10;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.MaSv).HasName("PK__SinhVien__2725081A7F6EF4DB");

            entity.ToTable("SinhVien");

            entity.Property(e => e.MaSv).HasColumnName("MaSV");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

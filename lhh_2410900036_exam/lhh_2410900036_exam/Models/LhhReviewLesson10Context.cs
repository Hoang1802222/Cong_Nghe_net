using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lhh_2410900036_exam.Models;

public partial class LhhReviewLesson10Context : DbContext
{
    public LhhReviewLesson10Context()
    {
    }

    public LhhReviewLesson10Context(DbContextOptions<LhhReviewLesson10Context> options)
        : base(options)
    {
    }

    public virtual DbSet<LhhEmploye> LhhEmployes { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS01;Database=LhhReviewLesson10;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LhhEmploye>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LhhEmplo__3214EC078C8D9AF7");

            entity.ToTable("LhhEmploye");

            entity.Property(e => e.LhhActive).HasColumnName("lhhActive");
            entity.Property(e => e.LhhBirthDay).HasColumnName("lhhBirthDay");
            entity.Property(e => e.LhhEmail)
                .HasMaxLength(100)
                .HasColumnName("lhhEmail");
            entity.Property(e => e.LhhGender)
                .HasMaxLength(100)
                .HasColumnName("lhhGender");
            entity.Property(e => e.LhhName)
                .HasMaxLength(100)
                .HasColumnName("lhhName");
            entity.Property(e => e.LhhPhone)
                .HasMaxLength(100)
                .HasColumnName("lhhPhone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

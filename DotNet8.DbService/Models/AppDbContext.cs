using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.DbService.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBackupMeterBill> TblBackupMeterBills { get; set; }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    public virtual DbSet<TblMeterBill> TblMeterBills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBackupMeterBill>(entity =>
        {
            entity.HasKey(e => e.MeterBillId);

            entity.ToTable("Tbl_BackupMeterBill");

            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserId)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.MeterBillCode)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.MeterBillFileData)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MeterBillFilePath)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserId)
                .HasMaxLength(225)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogContent).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<TblMeterBill>(entity =>
        {
            entity.HasKey(e => e.MeterBillId);

            entity.ToTable("Tbl_MeterBill");

            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserId)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.MeterBillCode)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.MeterBillFileData)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MeterBillFilePath)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserId)
                .HasMaxLength(225)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Azure.Models;

public partial class OneridbContext : DbContext
{
    public OneridbContext()
    {
    }

    public OneridbContext(DbContextOptions<OneridbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Diziler> Dizilers { get; set; }

    public virtual DbSet<DizilerVeKullanicilar> DizilerVeKullanicilars { get; set; }

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    public virtual DbSet<Roller> Rollers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=AzureConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diziler>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diziler__3214EC0771A4189C");

            entity.ToTable("Diziler");

            entity.Property(e => e.DiziAciklamasi)
                .HasMaxLength(255)
                .HasColumnName("Dizi Aciklamasi");
            entity.Property(e => e.DiziAdi)
                .HasMaxLength(50)
                .HasColumnName("Dizi Adi");
            entity.Property(e => e.Sure).HasMaxLength(30);
        });

        modelBuilder.Entity<DizilerVeKullanicilar>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DizilerVeKullanicilar");

            entity.Property(e => e.KullaniciId).HasMaxLength(512);

            entity.HasOne(d => d.Dizi).WithMany()
                .HasForeignKey(d => d.DiziId)
                .HasConstraintName("FK__DizilerVe__DiziI__793DFFAF");

            entity.HasOne(d => d.Kullanici).WithMany()
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("FK__DizilerVe__Kulla__7849DB76");
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kullanic__3214EC0785340526");

            entity.ToTable("Kullanicilar");

            entity.Property(e => e.Id).HasMaxLength(512);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.KullaniciAdi)
                .HasMaxLength(30)
                .HasColumnName("Kullanici Adi");
            entity.Property(e => e.ParolaH).HasMaxLength(512);
            entity.Property(e => e.RolId).HasColumnName("Rol Id");

            entity.HasOne(d => d.Rol).WithMany(p => p.Kullanicilars)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Kullanici__Rol I__6AEFE058");
        });

        modelBuilder.Entity<Roller>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roller__3214EC07D0B5B615");

            entity.ToTable("Roller");

            entity.Property(e => e.RolAdi)
                .HasMaxLength(20)
                .HasColumnName("Rol Adi");
        });
        modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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

    public virtual DbSet<Filmler> Filmlers { get; set; }

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
            entity.Property(e => e.SezonSayisi).HasColumnName("Sezon Sayisi");
        });

        modelBuilder.Entity<Filmler>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Filmler__3214EC0756886B6A");

            entity.ToTable("Filmler");

            entity.Property(e => e.FilmAciklamasi)
                .HasMaxLength(255)
                .HasColumnName("Film Aciklamasi");
            entity.Property(e => e.FilmAdi)
                .HasMaxLength(50)
                .HasColumnName("Film Adi");
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

            entity.HasMany(d => d.Films).WithMany(p => p.Kullanicis)
                .UsingEntity<Dictionary<string, object>>(
                    "DizilerVeKullanicilar",
                    r => r.HasOne<Filmler>().WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DizilerVe__FilmI__0880433F"),
                    l => l.HasOne<Kullanicilar>().WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DizilerVe__Kulla__078C1F06"),
                    j =>
                    {
                        j.HasKey("KullaniciId", "FilmId").HasName("PK__DizilerV__36C0256CD845C735");
                        j.ToTable("DizilerVeKullanicilar");
                        j.IndexerProperty<string>("KullaniciId").HasMaxLength(512);
                    });

            entity.HasMany(d => d.FilmsNavigation).WithMany(p => p.KullanicisNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmlerVeKullanicilar",
                    r => r.HasOne<Filmler>().WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FilmlerVe__FilmI__04AFB25B"),
                    l => l.HasOne<Kullanicilar>().WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FilmlerVe__Kulla__03BB8E22"),
                    j =>
                    {
                        j.HasKey("KullaniciId", "FilmId").HasName("PK__FilmlerV__36C0256CA7FBD50D");
                        j.ToTable("FilmlerVeKullanicilar");
                        j.IndexerProperty<string>("KullaniciId").HasMaxLength(512);
                    });
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

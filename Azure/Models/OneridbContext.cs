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

            entity.HasMany(d => d.Dizis).WithMany(p => p.Kullanicis)
                .UsingEntity<Dictionary<string, object>>(
                    "DizilerKullanicilar",
                    r => r.HasOne<Diziler>().WithMany()
                        .HasForeignKey("DiziId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DizilerKu__DiziI__1C873BEC"),
                    l => l.HasOne<Kullanicilar>().WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DizilerKu__Kulla__1B9317B3"),
                    j =>
                    {
                        j.HasKey("KullaniciId", "DiziId").HasName("PK__DizilerK__77CB8BE60ADF0003");
                        j.ToTable("DizilerKullanicilar");
                        j.IndexerProperty<string>("KullaniciId").HasMaxLength(512);
                    });

            entity.HasMany(d => d.Films).WithMany(p => p.Kullanicis)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmlerKullanicilar",
                    r => r.HasOne<Filmler>().WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FilmlerKu__FilmI__2057CCD0"),
                    l => l.HasOne<Kullanicilar>().WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FilmlerKu__Kulla__1F63A897"),
                    j =>
                    {
                        j.HasKey("KullaniciId", "FilmId").HasName("PK__FilmlerK__36C0256CD34445C1");
                        j.ToTable("FilmlerKullanicilar");
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

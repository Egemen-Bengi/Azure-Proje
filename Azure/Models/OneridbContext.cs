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

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    public virtual DbSet<Roller> Rollers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=AzureConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CyComputer.Models;

public partial class BdreactContext : DbContext
{
    public BdreactContext()
    {
    }

    public BdreactContext(DbContextOptions<BdreactContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2Q3LPM0\\SQLEXPRESS;DataBase=BDReact;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPer).HasName("PK__Persona__3D78D1102575A5F6");

            entity.ToTable("Persona");

            entity.Property(e => e.IdPer).HasColumnName("idPer");
            entity.Property(e => e.ApePer)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apePer");
            entity.Property(e => e.EstPer).HasColumnName("estPer");
            entity.Property(e => e.NomPer)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nomPer");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

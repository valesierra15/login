using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Trabajo1.Models;

namespace Trabajo1.Models;

public partial class Trabajo1Context : DbContext
{
    public Trabajo1Context()
    {
    }

    public Trabajo1Context(DbContextOptions<Trabajo1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07DF41C9ED");

            entity.Property(e => e.Pass)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Usuario");
            entity.Property(e => e.Intentos)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.estado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<Trabajo1.Models.Registrar> Registrar { get; set; } = default!;
}

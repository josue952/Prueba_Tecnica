using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Prueba_Tecnica.Models;

public partial class PruebaTecnicaContext : DbContext
{
    public PruebaTecnicaContext()
    {
    }

    public PruebaTecnicaContext(DbContextOptions<PruebaTecnicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__AEFFDBC5CD05F9C9");

            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.CodigoEstudiante)
                .HasColumnName("codigoEstudiante")
                .HasColumnType("int");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.DetallesBitacora)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detallesBitacora");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PK__Materias__4B740AB3747E2B94");

            entity.Property(e => e.IdMateria).HasColumnName("idMateria");

            entity.Property(e => e.CodigoEstudiante)
                .HasColumnName("codigoEstudiante")
                .HasColumnType("int");

            entity.Property(e => e.NombreMateria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreMateria");

            entity.Property(e => e.CodigoInstructor)
                .HasColumnName("codigoInstructor")
                .HasColumnType("int");

            entity.Property(e => e.Horario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("horario");

            entity.Property(e => e.Ubicacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ubicacion");

            entity.Property(e => e.DetallesBitacora)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detallesBitacora");
        });

        OnModelCreatingPartial(modelBuilder);
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}




using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BeneficiarioAPI.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beneficiario> Beneficiarios { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beneficiario>(entity =>
        {
            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Beneficiarios).HasConstraintName("FK_BeneficiarioEmpleado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

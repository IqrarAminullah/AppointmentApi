using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Models;

public partial class AppointmentDbContext : DbContext
{
    public AppointmentDbContext()
    {
    }

    public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MPLB0RV\\MSSQLSERVER01;Database=AppointmentDB;UID=Iqrar;PWD=iqrar;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2FDECB7FE");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.AppointmentEnd).HasColumnType("datetime");
            entity.Property(e => e.AppointmentStart).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Custo__2B3F6F97");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8E630F304");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

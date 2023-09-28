using System;
using System.Collections.Generic;
using AutoPAS_Customer_portal.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AutoPAS_Customer_portal.Data;

public partial class VMDbContext : DbContext
{
    public VMDbContext()
    {
    }

    public VMDbContext(DbContextOptions<VMDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bodytype> Bodytype { get; set; }

    public virtual DbSet<Brand> Brand { get; set; }

    public virtual DbSet<Fueltype> Fueltype { get; set; }

    public virtual DbSet<Model> Model { get; set; }

    public virtual DbSet<Policy> Policy { get; set; }

    public virtual DbSet<Policyvehicle> Policyvehicle { get; set; }

    public virtual DbSet<Rto> Rto { get; set; }

    public virtual DbSet<Transmissiontype> Transmissiontype { get; set; }

    public virtual DbSet<Variant> Variant { get; set; }

    public virtual DbSet<Vehicle> Vehicle { get; set; }

    public virtual DbSet<Vehicletype> Vehicletype { get; set; }

 /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseMySQL("server=VM-104; database=autopastraining;User Id=apastraining;Password=apastraining123.;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bodytype>(entity =>
        {
            entity.HasKey(e => e.BodyTypeId).HasName("PRIMARY");

            entity.ToTable("bodytype");

            entity.HasIndex(e => e.BodyTypeId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.BodyType1)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("BodyType");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PRIMARY");

            entity.ToTable("brand");

            entity.HasIndex(e => e.VehicleTypeId, "VehicleTypeId_idx");

            entity.HasIndex(e => e.BrandId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Brand1)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("Brand");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength();

            entity.HasOne(d => d.VehicleType).WithMany(p => p.Brands)
                .HasForeignKey(d => d.VehicleTypeId)
                .HasConstraintName("VehicleType2Id");
        });

        modelBuilder.Entity<Fueltype>(entity =>
        {
            entity.HasKey(e => e.FuelTypeId).HasName("PRIMARY");

            entity.ToTable("fueltype");

            entity.HasIndex(e => e.FuelTypeId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.FuelType1)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("FuelType");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PRIMARY");

            entity.ToTable("model");

            entity.HasIndex(e => e.BrandId, "BrandId_idx");

            entity.HasIndex(e => e.ModelId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Modelname)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("modelname");

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("Brand2Id");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PRIMARY");

            entity.ToTable("policy");

            entity.HasIndex(e => e.AppUserId, "Appuser2id_idx");

            entity.HasIndex(e => e.PolicyNumber, "PolicyNumber_UNIQUE").IsUnique();

            entity.HasIndex(e => e.QuoteNumber, "QuoteNumber_UNIQUE").IsUnique();

            entity.HasIndex(e => e.PolicyId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.PolicyId)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Cgst)
                .HasPrecision(10)
                .HasColumnName("CGST");
            entity.Property(e => e.EligibleForNcb)
                .HasDefaultValueSql("'0'")
                .HasColumnName("EligibleForNCB");
            entity.Property(e => e.Igst)
                .HasPrecision(10)
                .HasColumnName("IGST");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.PolicyEffectiveDt).HasColumnType("date");
            entity.Property(e => e.PolicyExpirationDt).HasColumnType("date");
            entity.Property(e => e.QuoteNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.RateDt).HasColumnType("date");
            entity.Property(e => e.ReceiptNumber)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.Sgst)
                .HasPrecision(10)
                .HasColumnName("SGST");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.TotalFees).HasPrecision(10);
            entity.Property(e => e.TotalPremium).HasPrecision(10);
        });

        modelBuilder.Entity<Policyvehicle>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("policyvehicle");

            entity.HasIndex(e => e.PolicyId, "fk_polid");

            entity.HasIndex(e => e.VehicleId, "fk_vehicleid");

            entity.Property(e => e.PolicyId)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.VehicleId)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Rto>(entity =>
        {
            entity.HasKey(e => e.Rtoid).HasName("PRIMARY");

            entity.ToTable("rto");

            entity.HasIndex(e => e.Rtoid, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Rtoid).HasColumnName("RTOId");
            entity.Property(e => e.City)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Rtoname)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("RTOName");
            entity.Property(e => e.State)
                .HasMaxLength(45)
                .IsFixedLength();
        });

        modelBuilder.Entity<Transmissiontype>(entity =>
        {
            entity.HasKey(e => e.TransmissionTypeId).HasName("PRIMARY");

            entity.ToTable("transmissiontype");

            entity.HasIndex(e => e.TransmissionTypeId, "Id_UNIQUE").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.TransmissionType1)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("TransmissionType");
        });

        modelBuilder.Entity<Variant>(entity =>
        {
            entity.HasKey(e => e.VariantId).HasName("PRIMARY");

            entity.ToTable("variant");

            entity.HasIndex(e => e.VariantId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Variant1)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Variant");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PRIMARY");

            entity.ToTable("vehicle");

            entity.HasIndex(e => e.BodyTypeId, "BodyId_idx");

            entity.HasIndex(e => e.BrandId, "BrandId_idx");

            entity.HasIndex(e => e.FuelTypeId, "FuelTypeId_idx");

            entity.HasIndex(e => e.ModelId, "ModelId_idx");

            entity.HasIndex(e => e.Rtoid, "RTOId_idx");

            entity.HasIndex(e => e.TransmissionTypeId, "TransmissionTypeId_idx");

            entity.HasIndex(e => e.VariantId, "VariantId_idx");

            entity.HasIndex(e => e.VehicleTypeid, "VehicleTypeId_idx");

            entity.HasIndex(e => e.VehicleId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.VehicleId)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.ChasisNumber)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.Color)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.DateOfPurchase).HasColumnType("date");
            entity.Property(e => e.EngineNumber)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.ExShowroomPrice).HasPrecision(10);
            entity.Property(e => e.Idv)
                .HasPrecision(10)
                .HasColumnName("IDV");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.Rtoid).HasColumnName("RTOId");

            entity.HasOne(d => d.BodyType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.BodyTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BodyId");

            entity.HasOne(d => d.Brand).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BrandId");

            entity.HasOne(d => d.FuelType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.FuelTypeId)
                .HasConstraintName("FuelTypeId");

            entity.HasOne(d => d.Model).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ModelId");

            entity.HasOne(d => d.Rto).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.Rtoid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RTOId");

            entity.HasOne(d => d.TransmissionType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.TransmissionTypeId)
                .HasConstraintName("TransmissionTypeId");

            entity.HasOne(d => d.Variant).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VariantId");

            entity.HasOne(d => d.VehicleType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleTypeid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VehicleTypeId");
        });

        modelBuilder.Entity<Vehicletype>(entity =>
        {
            entity.HasKey(e => e.VehicleTypeId).HasName("PRIMARY");

            entity.ToTable("vehicletype");

            entity.HasIndex(e => e.VehicleTypeId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.VehicleType1)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("VehicleType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/
}

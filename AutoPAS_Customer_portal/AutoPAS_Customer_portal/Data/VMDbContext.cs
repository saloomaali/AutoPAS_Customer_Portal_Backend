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

 
}

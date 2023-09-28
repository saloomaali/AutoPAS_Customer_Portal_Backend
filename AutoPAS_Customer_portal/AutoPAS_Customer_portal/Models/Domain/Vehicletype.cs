using System;
using System.Collections.Generic;

namespace AutoPAS_Customer_portal.Models.Domain;

public partial class Vehicletype
{
    public int VehicleTypeId { get; set; }

    public string VehicleType { get; set; }

    public string Description { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}

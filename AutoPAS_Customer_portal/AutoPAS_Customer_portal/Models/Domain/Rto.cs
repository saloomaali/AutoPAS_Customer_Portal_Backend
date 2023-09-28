using System;
using System.Collections.Generic;

namespace AutoPAS_Customer_portal.Models.Domain;

public partial class Rto
{
    public int Rtoid { get; set; }

    public string Rtoname { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Description { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}

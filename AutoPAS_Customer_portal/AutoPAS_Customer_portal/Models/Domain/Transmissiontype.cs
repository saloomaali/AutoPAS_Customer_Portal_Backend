using System;
using System.Collections.Generic;

namespace AutoPAS_Customer_portal.Models.Domain;

public partial class Transmissiontype
{
    public int TransmissionTypeId { get; set; }

    public string TransmissionType { get; set; }

    public string Description { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}

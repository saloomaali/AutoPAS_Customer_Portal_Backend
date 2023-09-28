using System;
using System.Collections.Generic;

namespace AutoPAS_Customer_portal.Models.Domain;

public partial class Model
{
    public int ModelId { get; set; }

    public int? BrandId { get; set; }

    public string Modelname { get; set; }

    public string Description { get; set; }

    public int? SortOrder { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}

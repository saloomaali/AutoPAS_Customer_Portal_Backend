using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoPAS_Customer_portal.Models.Domain;

[Keyless]
public partial class Policyvehicle
{
    public string PolicyId { get; set; }

    public string VehicleId { get; set; }
}

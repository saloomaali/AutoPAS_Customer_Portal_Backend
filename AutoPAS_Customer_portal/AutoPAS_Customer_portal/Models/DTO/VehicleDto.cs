namespace AutoPAS_Customer_portal.Models.DTO
{
    public class VehicleDto
    {
        public int? PolicyNumber { get; set; }
        public string VehicleType { get; set; }
        public string Rtoname { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string RegistrationNumber { get; set; }

        public DateTime? DateOfPurchase { get; set; }

        public string Brand { get; set; }

        public string Modelname { get; set; }

        public string Variant { get; set; }

        public string BodyType { get; set; }

        public string FuelType { get; set; }

        public string TransmissionType { get; set; }

        public string Color { get; set; }

        public string ChasisNumber { get; set; }

        public string EngineNumber { get; set; }

        public int? CubicCapacity { get; set; }

        public int? SeatingCapacity { get; set; }

        public int? YearOfManufacture { get; set; }


        public decimal? Idv { get; set; }

        public decimal? ExShowroomPrice { get; set; }

    }
}

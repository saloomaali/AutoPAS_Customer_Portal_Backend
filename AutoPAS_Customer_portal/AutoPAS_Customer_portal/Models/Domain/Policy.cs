using System;
using System.Collections.Generic;

namespace AutoPAS_Customer_portal.Models.Domain;

public partial class Policy
{
    public string PolicyId { get; set; }

    public int? AppUserId { get; set; }

    public int? PolicyNumber { get; set; }

    public int QuoteNumber { get; set; }

    public DateTime? PolicyEffectiveDt { get; set; }

    public DateTime? PolicyExpirationDt { get; set; }

    public string Status { get; set; }

    public int? Term { get; set; }

    public DateTime? RateDt { get; set; }

    public decimal? TotalPremium { get; set; }

    public decimal? Sgst { get; set; }

    public decimal? Cgst { get; set; }

    public decimal? Igst { get; set; }

    public decimal? TotalFees { get; set; }

    public string PaymentType { get; set; }

    public string ReceiptNumber { get; set; }

    public sbyte? EligibleForNcb { get; set; }
}

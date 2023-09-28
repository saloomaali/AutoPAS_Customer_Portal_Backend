using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoPAS_Customer_portal.Models.Domain
{
    public class UserPloicyList
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int PolicyNumber { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public PortalUser User { get; set; }
    }
}

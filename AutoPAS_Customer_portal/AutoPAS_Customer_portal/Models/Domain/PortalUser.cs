using System.Text.Json.Serialization;

namespace AutoPAS_Customer_portal.Models.Domain
{
    public class PortalUser
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public ICollection<UserPloicyList> PolicyList { get; set; }
    }
}

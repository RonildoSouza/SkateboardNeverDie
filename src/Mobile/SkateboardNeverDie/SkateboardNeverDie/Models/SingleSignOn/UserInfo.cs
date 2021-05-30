using System.Linq;
using System.Text.Json.Serialization;

namespace SkateboardNeverDie.Models
{
    public class UserInfo
    {
        [JsonPropertyName("sub")]
        public string Id { get; set; }
        public string Email { get; set; }
        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }
        public string[] Role { get; set; }

        public bool IsAdmin() => Role?.Contains("Admin") ?? false;
    }
}

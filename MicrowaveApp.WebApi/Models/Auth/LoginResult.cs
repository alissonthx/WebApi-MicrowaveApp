using System.Text.Json.Serialization;

namespace MicrowaveApp.WebApi.Models.Auth
{
    public class LoginResult
    {
        public DateTime ExpiresAt { get; set; }
        public required string Token { get; set; }
        public bool IsSuccess { get; set; }

        [JsonIgnore] 
        public string? UserId { get; set; }
    }
}
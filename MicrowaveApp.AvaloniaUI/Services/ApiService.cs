using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicrowaveApp.AvaloniaUI.Services
{
    public class ApiService
    {
        private readonly HttpClient _client = new();
        private readonly string _baseUrl = "http://localhost:5039/api";

        public async Task<bool> LoginAndSetToken()
        {
            var login = new { Username = "", Password = "" };
            var json = JsonSerializer.Serialize(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_baseUrl}/Auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<JsonElement>(result).GetProperty("Token").GetString();
                _client.DefaultRequestHeaders.Authorization = new("Bearer", token);
                return true;
            }
            return false;
        }

        public async Task<bool> StartMicrowave(int seconds, int power)
        {
            var data = new { TimeInSeconds = seconds, Power = power };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_baseUrl}/Microwave/start", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> StopMicrowave()
        {
            var response = await _client.PostAsync($"{_baseUrl}/Microwave/stop", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetStatus()
        {
            var response = await _client.GetAsync($"{_baseUrl}/Microwave/status");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "Error getting status";
        }
    }
}
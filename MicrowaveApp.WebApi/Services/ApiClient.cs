using MicrowaveApp.WebApi.Models.Auth;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace MicrowaveApp.WebApi.Services
{
    public class ApiClient
    {
        private readonly HttpClient _client;
        private string _token = "";

        public ApiClient(string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentException("É necessário passar um endereço base", nameof(baseAddress));
            }
            else
            {
                _client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var login = new { Username = username, Password = HashPassword(password) };
            var response = await _client.PostAsJsonAsync("api/auth/login", login);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                if (result == null)
                {
                    return false;
                }
                else
                {
                    _token = result.Token;
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    return true;
                }
            }

            return false;
        }

        private string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        public async Task StartHeatingAsync(int time, int power)
        {
            var request = new { TimeInSeconds = time, Power = power };
            await _client.PostAsJsonAsync("api/microwave/start", request);
        }
    }
}

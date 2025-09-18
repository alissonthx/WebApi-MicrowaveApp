using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MicrowaveApp.Business.Models;

namespace MicrowaveApp.AvaloniaUI.Services
{
    public class ApiService
    {
        private readonly HttpClient _client = new();
        private readonly string _baseUrl = "http://localhost:5039/api";

        public async Task<bool> LoginAndSetToken(string username, string password)
        {
            try
            {
                var login = new { Username = username, Password = password };
                var json = JsonSerializer.Serialize(login);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_baseUrl}/Auth/login", content);
                var result = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Login API Response: " + result);

                if (response.IsSuccessStatusCode)
                {
                    using var doc = JsonDocument.Parse(result);
                    if (doc.RootElement.TryGetProperty("token", out var tokenElement))
                    {
                        var token = tokenElement.GetString();
                        _client.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Token not found in response.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Login failed: " + result);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during login: " + ex.Message);
                return false;
            }
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

        public async Task<PredefinedProgram[]> GetPredefinedPrograms()
        {
            var response = await _client.GetAsync($"{_baseUrl}/Programs/predefined");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PredefinedProgram[]>(json);
        }

        public async Task<CustomProgram[]> GetCustomPrograms()
        {
            var response = await _client.GetAsync($"{_baseUrl}/Programs/custom");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CustomProgram[]>(json);
        }

        // public async Task<string> GetStatus()
        // {
        //     var response = await _client.GetAsync($"{_baseUrl}/Microwave/status");
        //     if (response.IsSuccessStatusCode)
        //     {
        //         return await response.Content.ReadAsStringAsync();
        //     }
        //     return "Erro ao checar status";
        // }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Avalonia.Controls;
using System;
using MicrowaveApp.AvaloniaUI.Views;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace MicrowaveApp.AvaloniaUI.ViewModels
{
    public partial class LoginWindowViewModel : ObservableObject
    {
        private string _token;
        private readonly HttpClient _httpClient;
        private Window? _window;

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _isLoading = false;

        public LoginWindowViewModel()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/") // Adjust based on your API
            };
        }

        public void SetWindow(Window window)
        {
            _window = window;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Por favor insira login e senha";
                return;
            }

            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                var loginData = new
                {
                    Username,
                    Password
                };

                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var authResponse = JsonSerializer.Deserialize<AuthResponse>(
                        responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (authResponse?.Success == true)
                    {
                        // Store on frontend token
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

                        var mainWindow = new MainWindow();
                        mainWindow.Show();

                        _window?.Close();
                    }
                    else
                    {
                        ErrorMessage = authResponse?.Message ?? "Login falhou";
                    }
                }
                else
                {
                    ErrorMessage = "Senha ou usuário inválidos";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void Exit()
        {
            _window?.Close();
            Environment.Exit(0);
        }
    }

    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
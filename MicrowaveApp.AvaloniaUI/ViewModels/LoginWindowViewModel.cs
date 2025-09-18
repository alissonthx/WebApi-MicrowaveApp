using ReactiveUI;
using System.Reactive;
using MicrowaveApp.AvaloniaUI.Services;
using System;

namespace MicrowaveApp.AvaloniaUI.ViewModels
{
    public class LoginWindowViewModel : ReactiveObject
    {
        private readonly ApiService _api = new();
        private string _username = "admin";
        private string _password = "admin123";
        private string _status = "Insira as credenciais e clique em Login";
        private bool _isLoggingIn = false;

        public LoginWindowViewModel()
        {
            LoginCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsLoggingIn = true;
                Status = "Logando ...";
                
                var success = await _api.LoginAndSetToken();
                
                if (success)
                {
                    Status = "Login com sucesso!";
                    LoginSuccessful?.Invoke(_api);
                }
                else
                {
                    Status = "Login falhou. Tente Novamente.";
                }
                
                IsLoggingIn = false;
            });
        }

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set => this.RaiseAndSetIfChanged(ref _isLoggingIn, value);
        }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }
        
        public event Action<ApiService>? LoginSuccessful;
    }
}
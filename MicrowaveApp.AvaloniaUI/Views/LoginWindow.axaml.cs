using Avalonia.Controls;
using MicrowaveApp.AvaloniaUI.ViewModels;
using MicrowaveApp.AvaloniaUI.Services;

namespace MicrowaveApp.AvaloniaUI.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            var viewModel = new LoginWindowViewModel();
            DataContext = viewModel;
            
            // When login is successful, open main window and close this one
            viewModel.LoginSuccessful += OnLoginSuccessful;
        }
        
        private void OnLoginSuccessful(ApiService apiService)
        {
            var mainWindow = new MainWindow(apiService);
            mainWindow.Show();
            this.Close();
        }
    }
}
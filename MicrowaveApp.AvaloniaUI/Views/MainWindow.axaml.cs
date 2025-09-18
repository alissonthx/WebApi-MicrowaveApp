using Avalonia.Controls;
using MicrowaveApp.AvaloniaUI.ViewModels;
using MicrowaveApp.AvaloniaUI.Services;

namespace MicrowaveApp.AvaloniaUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(ApiService apiService)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(apiService);
        }
    }
}
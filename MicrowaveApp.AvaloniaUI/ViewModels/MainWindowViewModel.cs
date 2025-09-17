using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Threading;
using MicrowaveApp.Business;

namespace MicrowaveApp.AvaloniaUI.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly Microwave _microwave = new Microwave();
        private int _initialTimeInSeconds = 0;
        private readonly Timer _timer;

        // Bindable Properties
        [ObservableProperty]
        private string _displayTime = "00:00";

        [ObservableProperty]
        private string _displayPower = "10";

        [ObservableProperty]
        private bool _isRunning = false;

        [ObservableProperty]
        private bool _isPaused = false;

        [ObservableProperty]
        private int _progressValue = 0;

        [ObservableProperty]
        private string _instructions = "";

        public MainWindowViewModel()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed!;
            _timer.AutoReset = true;
            
            // Subscribe to microwave events
            _microwave.HeatingProgressChanged += OnHeatingProgressChanged;
            _microwave.HeatingStarted += OnHeatingStarted;
            _microwave.HeatingPaused += OnHeatingPaused;
            _microwave.HeatingResumed += OnHeatingResumed;
            _microwave.HeatingCanceled += OnHeatingCanceled;
            _microwave.HeatingFinished += OnHeatingFinished;
        }

        // Event handlers
        private void OnHeatingProgressChanged(string progress)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                Instructions = progress;
            });
        }

        private void OnHeatingStarted()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsRunning = true;
                IsPaused = false;
            });
        }

        private void OnHeatingPaused()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsPaused = true;
            });
        }

        private void OnHeatingResumed()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsPaused = false;
            });
        }

        private void OnHeatingCanceled()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsRunning = false;
                IsPaused = false;
                DisplayTime = "00:00";
                ProgressValue = 0;
            });
        }

        private void OnHeatingFinished()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsRunning = false;
                IsPaused = false;
                DisplayTime = "00:00";
                ProgressValue = 100;
                // Play sound logic here
            });
        }

        [RelayCommand]
        private void AddNumber(string number)
        {
            if (!_microwave.IsRunning)
            {
                // Convert current time to seconds, add the number, and set back
                int currentSeconds = TimeStringToSeconds(DisplayTime);
                int newSeconds = currentSeconds * 10 + int.Parse(number);
                
                if (newSeconds <= 120) // Max 2 minutes
                {
                    DisplayTime = Microwave.FormatTime(newSeconds);
                }
            }
        }

        [RelayCommand]
        private void Start()
        {
            if (!_microwave.IsRunning)
            {
                int timeInSeconds = TimeStringToSeconds(DisplayTime);
                int power = int.Parse(DisplayPower);
                
                if (timeInSeconds > 0)
                {
                    _initialTimeInSeconds = timeInSeconds;
                    _microwave.StartHeating(timeInSeconds, power);
                }
            }
        }

        [RelayCommand]
        private void Stop()
        {
            _microwave.PauseOrCancel();
        }

        [RelayCommand]
        private void Pause()
        {
            if (_microwave.IsRunning && !_microwave.IsPaused)
            {
                _microwave.PauseOrCancel();
            }
            else if (_microwave.IsPaused)
            {
                // Resume heating
                int timeInSeconds = TimeStringToSeconds(DisplayTime);
                int power = int.Parse(DisplayPower);
                _microwave.StartHeating(timeInSeconds, power);
            }
        }

        [RelayCommand]
        private void SetPower()
        {
            if (!_microwave.IsRunning)
            {
                int currentPower = int.Parse(DisplayPower);
                int newPower = (currentPower % 10) + 1;
                DisplayPower = newPower.ToString();
            }
        }

        [RelayCommand]
        private void QuickStart()
        {
            _microwave.QuickStart();
            DisplayTime = "00:30";
            DisplayPower = "10";
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
        }

        private int TimeStringToSeconds(string timeString)
        {
            if (timeString.Contains(":"))
            {
                var parts = timeString.Split(':');
                int minutes = int.Parse(parts[0]);
                int seconds = int.Parse(parts[1]);
                return minutes * 60 + seconds;
            }
            else if (timeString.EndsWith("s"))
            {
                return int.Parse(timeString.TrimEnd('s'));
            }
            return int.Parse(timeString);
        }
    }
}
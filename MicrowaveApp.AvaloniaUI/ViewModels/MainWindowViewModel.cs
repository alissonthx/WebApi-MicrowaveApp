using ReactiveUI;
using System.Reactive;
using MicrowaveApp.AvaloniaUI.Services;

namespace MicrowaveApp.AvaloniaUI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly ApiService _api;
        private string _status = "Ready to use microwave";
        private int _time = 30;
        private int _power = 50;

        public MainWindowViewModel(ApiService api)
        {
            _api = api;

            StartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (await _api.StartMicrowave(Time, Power))
                {
                    Status = $"Iniciou à: {Time}s em {Power}%";
                }
                else
                {
                    Status = "Início falhou";
                }
            });

            StopCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (await _api.StopMicrowave())
                {
                    Status = "Pausado";
                }
                else
                {
                    Status = "Pausa falhou";
                }
            });

            StatusCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                Status = await _api.GetStatus();
            });
        }

        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public int Time
        {
            get => _time;
            set => this.RaiseAndSetIfChanged(ref _time, value);
        }

        public int Power
        {
            get => _power;
            set => this.RaiseAndSetIfChanged(ref _power, value);
        }

        public ReactiveCommand<Unit, Unit> StartCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> StatusCommand { get; }
    }
}
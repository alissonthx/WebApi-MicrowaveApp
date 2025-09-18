using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using MicrowaveApp.AvaloniaUI.Services;
using MicrowaveApp.Business.Models;
using System.Reactive.Linq;

namespace MicrowaveApp.AvaloniaUI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly ApiService _api;

        private string _status = "Pronto para usar o micro-ondas";
        private int _time = 30;
        private int _power = 10;

        private string _authStatus;
        public string AuthStatus
        {
            get => _authStatus;
            set => this.RaiseAndSetIfChanged(ref _authStatus, value);
        }

        private CustomProgram _selectedProgram;

        // Single Programs collection for XAML ComboBox
        public ObservableCollection<CustomProgram> Programs { get; } = new();

        // Heating progress text
        private string _heatingProgress;
        public string HeatingProgress
        {
            get => _heatingProgress;
            set => this.RaiseAndSetIfChanged(ref _heatingProgress, value);
        }

        // Selected program
        public CustomProgram SelectedProgram
        {
            get => _selectedProgram;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedProgram, value);
                if (value != null)
                {
                    Time = value.TimeInSeconds;
                    Power = value.Power;
                    SelectedProgramInstructions = value.Instructions;
                }
            }
        }

        private string _selectedProgramInstructions;
        public string SelectedProgramInstructions
        {
            get => _selectedProgramInstructions;
            set => this.RaiseAndSetIfChanged(ref _selectedProgramInstructions, value);
        }

        // Custom Program inputs
        public string CustomName { get; set; }
        public string CustomFood { get; set; }
        public int CustomTime { get; set; }
        public int CustomPower { get; set; }
        public char CustomChar { get; set; }
        public string CustomInstructions { get; set; }

        // Input bindings
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

        // Commands
        public ReactiveCommand<Unit, Unit> QuickStartCommand { get; }
        public ReactiveCommand<Unit, Unit> StartCommand { get; }
        public ReactiveCommand<Unit, Unit> PauseCancelCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCustomProgramCommand { get; }
        public ReactiveCommand<Unit, Unit> LoadProgramsCommand { get; }

        public MainWindowViewModel(ApiService api)
        {
            _api = api;

            LoadProgramsCommand = ReactiveCommand.CreateFromTask(LoadPrograms);

            StartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (SelectedProgram != null)
                {
                    Time = SelectedProgram.TimeInSeconds;
                    Power = SelectedProgram.Power;
                }

                if (await _api.StartMicrowave(Time, Power))
                    HeatingProgress = $"Aquecimento iniciado: {Time}s em potência {Power}";
                else
                    HeatingProgress = "Falha ao iniciar o aquecimento";
            });

            PauseCancelCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (await _api.StopMicrowave())
                    HeatingProgress = "Aquecimento pausado";
                else
                    HeatingProgress = "Falha ao pausar o aquecimento";
            });

            QuickStartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                Time = 30;
                Power = 10;
                await StartCommand.Execute();
            });

            AddCustomProgramCommand = ReactiveCommand.Create(() =>
            {
                var newProgram = new CustomProgram
                {
                    Name = CustomName,
                    Food = CustomFood,
                    TimeInSeconds = CustomTime,
                    Power = CustomPower,
                    HeatingCharacter = CustomChar,
                    Instructions = CustomInstructions,
                    IsPredefined = false
                };
                Programs.Add(newProgram);

                // Clear inputs
                CustomName = CustomFood = CustomInstructions = "";
                CustomTime = 0;
                CustomPower = 0;
                CustomChar = '*';
            });
        }

        private async Task LoadPrograms()
        {
            Programs.Clear();
            var predefined = await _api.GetPredefinedPrograms();
            var custom = await _api.GetCustomPrograms();

            foreach (var p in predefined)
                Programs.Add(new CustomProgram
                {
                    Name = p.Name,
                    Food = p.Food,
                    TimeInSeconds = p.TimeInSeconds,
                    Power = p.Power,
                    HeatingCharacter = p.HeatingCharacter,
                    Instructions = p.Instructions,
                    IsPredefined = true
                });

            foreach (var c in custom)
                Programs.Add(c);
        }
    }
}

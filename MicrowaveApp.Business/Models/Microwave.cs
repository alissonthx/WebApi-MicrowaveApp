using MicrowaveApp.Business.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicrowaveApp.Business
{
    public class Microwave
    {
        private int _power;
        private int _timeInSeconds;
        private bool _isRunning;
        private bool _isPaused;
        private int _remainingTime;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action<string> HeatingProgressChanged;
        public event Action HeatingStarted;
        public event Action HeatingPaused;
        public event Action HeatingResumed;
        public event Action HeatingCanceled;
        public event Action HeatingFinished;

        private char _heatingCharacter = '.';
        private PredefinedProgram _currentProgram;

        public int Power
        {
            get => _power;
            set => _power = ValidatePower(value);
        }

        public int TimeInSeconds
        {
            get => _timeInSeconds;
            set => _timeInSeconds = ValidateTime(value);
        }

        public bool IsRunning => _isRunning;
        public bool IsPaused => _isPaused;

        public Microwave()
        {
            _power = 10; // Default power
        }

        public void StartPredefinedProgram(PredefinedProgram program)
        {
            if (_isRunning && !_isPaused)
                throw new InvalidOperationException("Já existe um aquecimento em andamento");

            _currentProgram = program;
            _heatingCharacter = program.HeatingCharacter;
            StartHeating(program.TimeInSeconds, program.Power);
        }

        public void StartHeating(int timeInSeconds, int power = 10)
        {
            if (_isRunning && !_isPaused)
            {
                AddTime(30);
                return;
            }

            TimeInSeconds = timeInSeconds;
            Power = power;
            _remainingTime = TimeInSeconds;

            if (_isPaused)
            {
                ResumeHeating();
                return;
            }

            StartNewHeating();
        }

        public void QuickStart()
        {
            StartHeating(30, 10);
        }

        public void PauseOrCancel()
        {
            if (_isRunning && !_isPaused)
            {
                PauseHeating();
            }
            else if (_isPaused)
            {
                CancelHeating();
            }
            else
            {
                // Clear if not running
                TimeInSeconds = 0;
                Power = 10;
                HeatingProgressChanged?.Invoke(string.Empty);
            }
        }

        private void StartNewHeating()
        {
            _isRunning = true;
            _isPaused = false;
            _cancellationTokenSource = new CancellationTokenSource();

            HeatingStarted?.Invoke();

            Task.Run(() => HeatAsync(_cancellationTokenSource.Token));
        }

        private async Task HeatAsync(CancellationToken cancellationToken)
        {
            while (_remainingTime > 0 && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                    break;

                _remainingTime--;
                UpdateHeatingProgress();
            }

            if (_remainingTime == 0 && !cancellationToken.IsCancellationRequested)
            {
                HeatingFinished?.Invoke();
                _isRunning = false;
            }
        }

        private void UpdateHeatingProgress()
        {
            int dotsPerSecond = Power;
            string progress = string.Empty;
            int charsPerSecond = Power;

            for (int i = 0; i < charsPerSecond; i++)
            {
                progress += _heatingCharacter;
            }

            for (int i = 0; i < dotsPerSecond; i++)
            {
                progress += ".";
            }

            progress += $" ({FormatTime(_remainingTime)})";

            if (_remainingTime == 0)
            {
                progress += " Aquecimento concluído";
            }

            HeatingProgressChanged?.Invoke(progress);
        }

        private void PauseHeating()
        {
            _isPaused = true;
            _cancellationTokenSource?.Cancel();
            HeatingPaused?.Invoke();
        }

        private void ResumeHeating()
        {
            _isPaused = false;
            StartNewHeating();
        }

        private void CancelHeating()
        {
            _isRunning = false;
            _isPaused = false;
            _cancellationTokenSource?.Cancel();
            HeatingCanceled?.Invoke();
        }

        public bool IsPredefinedProgramRunning() => _currentProgram != null;

        private void AddTime(int secondsToAdd)
        {
            int newTime = _remainingTime + secondsToAdd;
            _remainingTime = Math.Min(newTime, 120); // Max 2 minutes
        }

        private int ValidatePower(int power)
        {
            if (power < 1 || power > 10)
            {
                throw new Exceptions.InvalidPowerException("Potência deve estar entre 1 e 10");
            }
            return power;
        }

        private int ValidateTime(int timeInSeconds)
        {
            if (timeInSeconds < 1 || timeInSeconds > 120)
            {
                throw new Exceptions.InvalidTimeException("Tempo deve estar entre 1 segundo e 2 minutos");
            }
            return timeInSeconds;
        }

        public static string FormatTime(int seconds)
        {
            if (seconds < 60) return $"{seconds}s";

            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            return $"{minutes}:{remainingSeconds:D2}";
        }
    }
}

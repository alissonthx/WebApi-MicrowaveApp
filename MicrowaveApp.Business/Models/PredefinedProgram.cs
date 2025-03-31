
namespace MicrowaveApp.Business.Models
{
    public class PredefinedProgram
    {
        public string Name { get; }
        public string Food { get; }
        public int TimeInSeconds { get; }
        public int Power { get; }
        public char HeatingCharacter { get; }
        public string Instructions { get; }

        public PredefinedProgram(string name, string food, int timeInSeconds,int power, char heatingCharacter, string instructions)
        {
            Name = name;
            Food = food;
            TimeInSeconds = timeInSeconds;
            Power = power;
            HeatingCharacter = heatingCharacter;
            Instructions = instructions;
        }
    }    
}

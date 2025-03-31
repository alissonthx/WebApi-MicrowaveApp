
namespace MicrowaveApp.Business.Models
{
    public class CustomProgram
    {
        public string Name { get; set; }
        public string Food { get; set; }
        public int TimeInSeconds { get; set; }
        public int Power { get; set; }
        public char HeatingCharacter { get; set; }
        public string Instructions { get; set; }
        public bool IsPredefined { get; set; } = false;
    }  
}

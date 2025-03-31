using MicrowaveApp.Business.Models;
using System.Collections.Generic;

namespace MicrowaveApp.Business.Interfaces
{
    public interface IProgramRepository
    {
        void AddCustomProgram(CustomProgram program);
        IEnumerable<CustomProgram> GetAllCustomPrograms();
        void RemoveCustomProgram(string name);
    }
}

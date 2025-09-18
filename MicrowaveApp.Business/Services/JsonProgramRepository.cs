using MicrowaveApp.Business.Models;
using MicrowaveApp.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace MicrowaveApp.Business.Services
{
    public class JsonProgramRepository : IProgramRepository
    {
        private readonly string _filePath;

        public JsonProgramRepository(string filePath = "programs.json")
        {
            _filePath = filePath;
        }

        public void AddCustomProgram(CustomProgram program)
        {
            var programs = GetAllCustomPrograms().ToList();

            // Validate unique character
            if (programs.Any(p => p.HeatingCharacter == program.HeatingCharacter) ||
                program.HeatingCharacter == '.')
            {
                throw new InvalidOperationException("Caráctere de aquecimento já está em uso");
            }

            programs.Add(program);
            SavePrograms(programs);
        }

        public IEnumerable<CustomProgram> GetAllCustomPrograms()
        {
            if (!File.Exists(_filePath)) return Enumerable.Empty<CustomProgram>();

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<CustomProgram>>(json) ?? Enumerable.Empty<CustomProgram>();
        }

        public void RemoveCustomProgram(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("O identificador do programa não pode ser nulo ou vazio");

            var programs = GetAllCustomPrograms().ToList();
            CustomProgram programToRemove;

            // heating character search
            if (name.Length == 1)
            {
                programToRemove = programs.FirstOrDefault(p => p.HeatingCharacter == name[0]);
            }
            else
            {
                // by name (case-insensitive)
                programToRemove = programs.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }

            if (programToRemove == null)
            {
                throw new KeyNotFoundException(
                    name.Length == 1
                        ? $"Programa com caractere de aquecimento '{name}' não encontrado"
                        : $"Programa com nome '{name}' não encontrado");
            }

            programs.Remove(programToRemove);
            SavePrograms(programs);
        }

        private void SavePrograms(IEnumerable<CustomProgram> programs)
        {
            var json = JsonConvert.SerializeObject(programs, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}

using MicrowaveApp.Business.Interfaces;
using MicrowaveApp.Business.Models;
using System;
using System.Collections.Generic;

namespace MicrowaveApp.Business.Services
{
    public class ProgramService
    {
        private readonly List<PredefinedProgram> _predefinedPrograms;
        private readonly IProgramRepository _repository;
        public IReadOnlyList<PredefinedProgram> GetPredefinedPrograms() => _predefinedPrograms.AsReadOnly(); //Keeps list immutable to external code
        public IEnumerable<CustomProgram> GetCustomPrograms() => _repository.GetAllCustomPrograms();

        public ProgramService(IProgramRepository repository)
        {
            _repository = repository;
            _predefinedPrograms = new List<PredefinedProgram>
        {
            new PredefinedProgram("Pipoca", "Pipoca (de microondas)", 180, 7, '*', "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."),
            new PredefinedProgram("Leite", "Leite", 300, 5, '~', "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."),
            new PredefinedProgram("Carnes de boi","Carne em pedaço ou fatias",840, 4,'$', "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."),
            new PredefinedProgram("Frango","Frango (qualquer corte)",480, 7,'§', "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."),
            new PredefinedProgram("Feijão","Feijão congelado",480, 9,'@', "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.")
        };
        }

        public void AddCustomProgram(CustomProgram program)
        {
            if (string.IsNullOrWhiteSpace(program.Name)) throw new ArgumentException("Nome é obrigatório");
            if (string.IsNullOrWhiteSpace(program.Food)) throw new ArgumentException("Alimento é obrigatório");
            if (program.TimeInSeconds <= 0) throw new ArgumentException("Tempo deve ser positivo");
            if (program.Power < 1 || program.Power > 10) throw new ArgumentException("Potência inválida");

            _repository.AddCustomProgram(program);
        }
    }
}

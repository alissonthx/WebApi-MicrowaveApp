using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using MicrowaveApp.Business;
using MicrowaveApp.Business.Exceptions;
using MicrowaveApp.Business.Services;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MicrowaveApp.Tests
{
    [TestClass]
    public class MicrowaveTests
    {
        private Microwave _microwave;
        private string _lastProgress;
        private bool _heatingFinished;

        [TestInitialize]
        public void Setup()
        {
            _microwave = new Microwave();
            _microwave.HeatingProgressChanged += (progress) => _lastProgress = progress;
            _microwave.HeatingFinished += () => _heatingFinished = true;
        }

        [TestMethod]
        public void StartHeating_ValidParameters_StartsHeating()
        {
            _microwave.StartHeating(10, 5);
            Assert.IsTrue(_microwave.IsRunning);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTimeException))]
        public void StartHeating_TimeTooShort_InvalidTimeException()
        {
            _microwave.StartHeating(0, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPowerException))]
        public void StartHeating_PowerTooHigh_InvalidPowerException()
        {
            _microwave.StartHeating(10, 11);
        }

        [TestMethod]
        public void QuickStart_Sets30SecondsAndPower10()
        {
            _microwave.QuickStart();
            Assert.AreEqual(10, _microwave.Power);
            Assert.AreEqual(30, _microwave.TimeInSeconds);
        }

        [TestMethod]
        public async Task HeatingProgress_UpdatesCorrectly()
        {
            _microwave.StartHeating(2, 3); // 2 seconds, power 3
            await Task.Delay(1100); // Wait for first second

            Assert.IsTrue(_lastProgress.Contains("..."));
            Assert.IsFalse(_heatingFinished);

            await Task.Delay(1100); // Wait for second second
            Assert.IsTrue(_heatingFinished);
        }

        [TestMethod]
        public void PauseAndResume_WorksCorrectly()
        {
            _microwave.StartHeating(30, 5);
            _microwave.PauseOrCancel();

            Assert.IsTrue(_microwave.IsPaused);

            _microwave.StartHeating(30, 5); // This should resume
            Assert.IsTrue(_microwave.IsRunning);
            Assert.IsFalse(_microwave.IsPaused);
        }

        [TestClass]
        public class JsonProgramRepositoryTests
        {
            private const string TestFilePath = "test_programs.json";
            private JsonProgramRepository _repository;

            [TestInitialize]
            public void Setup()
            {
                // Create test file with sample data
                File.WriteAllText(TestFilePath,
                    @"[
                {""Name"":""Pipoca"",""HeatingCharacter"":""P"",""Time"":120,""Power"":8},
                {""Name"":""Refeição"",""HeatingCharacter"":""R"",""Time"":160,""Power"":9}
            ]");

                _repository = new JsonProgramRepository(TestFilePath);
            }

            [TestCleanup]
            public void Cleanup()
            {
                if (File.Exists(TestFilePath))
                    File.Delete(TestFilePath);
            }

            [TestMethod]
            public void RemoveCustomProgram_ByName_RemovesProgram()
            {
                // Act
                _repository.RemoveCustomProgram("Pipoca");

                // Assert
                var remaining = _repository.GetAllCustomPrograms().ToList();
                Assert.AreEqual(1, remaining.Count);
                Assert.IsFalse(remaining.Any(p => p.Name == "Pipoca"));
            }

            [TestMethod]
            public void RemoveCustomProgram_ByHeatingCharacter_RemovesProgram()
            {
                // Act
                _repository.RemoveCustomProgram("P"); // Single character treats as heating char

                // Assert
                var remaining = _repository.GetAllCustomPrograms().ToList();
                Assert.AreEqual(1, remaining.Count);
                Assert.IsFalse(remaining.Any(p => p.HeatingCharacter == 'P'));
            }

            [TestMethod]
            public void RemoveCustomProgram_NameCaseInsensitive_RemovesProgram()
            {
                // Act
                _repository.RemoveCustomProgram("pIpOcA"); // Mixed case

                // Assert
                var remaining = _repository.GetAllCustomPrograms().ToList();
                Assert.AreEqual(1, remaining.Count);
            }

            [TestMethod]
            [ExpectedException(typeof(KeyNotFoundException))]
            public void RemoveCustomProgram_InvalidName_ThrowsKeyNotFound()
            {
                _repository.RemoveCustomProgram("Nonexistent");
            }

            [TestMethod]
            [ExpectedException(typeof(KeyNotFoundException))]
            public void RemoveCustomProgram_InvalidHeatingChar_ThrowsKeyNotFound()
            {
                _repository.RemoveCustomProgram("X"); // Not in test data
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void RemoveCustomProgram_EmptyString_ThrowsArgumentException()
            {
                _repository.RemoveCustomProgram("");
            }          
        }
    }
}

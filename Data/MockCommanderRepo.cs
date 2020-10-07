using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<command> GetAllCommands()
        {
            var commands = new List<command>{
                new command { Id = 0, HowTo = "Be a developer", Line = "Follow tutorials", Platform = "Instructor and Resources" },
                new command { Id = 1, HowTo = "Be a scientist", Line = "Follow science", Platform = "Experiment and Researches" },
                new command { Id = 2, HowTo = "Be a doctor", Line = "Get Degree", Platform = "Medical Instituta and Trainings" },
                new command { Id = 3, HowTo = "Be a gentleman", Line = "Be kind", Platform = "Discipline and wisdom" },
            };
            return commands;
        }

        public command GetCommandById(int id)
        {
            return new command { Id = 0, HowTo = "Be a developer", Line = "Follow tutorials", Platform = "Instructor and Resources" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
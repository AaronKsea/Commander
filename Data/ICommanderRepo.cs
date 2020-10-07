using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<command> GetAllCommands();
        command GetCommandById(int id);
        void CreateCommand(command cmd);
        bool SaveChanges();
        void UpdateCommand(command cmd);
        void DeleteCommand(command command);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS_Microservice.Models;

namespace TMS_Microservice.Repository.IRepository
{
    public interface ISubtaskRepository
    {
        ICollection<Subtask> GetSubtasks();
        Subtask GetSubtask(int subtaskId);
        bool CreateSubtask(Subtask subtask);
        bool UpdateSubtask(Subtask subtask);
        bool DeleteSubtask(int id);
        bool Save();
    }
}

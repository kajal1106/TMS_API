using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS_Microservice.Models;

namespace TMS_Microservice.Data.Repository.IRepository
{
    interface ISubtaskRepository
    {
        ICollection<Subtask> GetSubtasks();
        Subtask GetSubtask(int subtaskId);
        bool CreateSubtask(Subtask subtask);
        bool UpdateSubtask(Subtask subtask);
        bool DeleteSubtask(Subtask subtask);
        bool Save();
    }
}

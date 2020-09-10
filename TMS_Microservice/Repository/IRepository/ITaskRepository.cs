using System;
using System.Collections.Generic;
using System.Linq;
using TMS_Microservice.Models;

namespace TMS_Microservice.Repository.IRepository
{
    public interface ITaskRepository
    {
        ICollection<Task> GetTasks();
        Task GetTask(int taskId);
        bool CreateTask(Task task);
        bool UpdateTask(Task task);
        bool DeleteTask(Task task);
        bool Save();
    }
}

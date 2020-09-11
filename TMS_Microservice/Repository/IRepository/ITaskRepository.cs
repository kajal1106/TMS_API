using System;
using System.Collections.Generic;
using System.Linq;
using TMS_Microservice.Models;

namespace TMS_Microservice.Repository.IRepository
{
    public interface ITaskRepository
    {
        ICollection<Task> GetTasks();
        ICollection<Task> GetTasksDate(DateTime date);
        Task GetTask(int taskId);
        bool TaskExists(int taskId);
        bool CreateTask(Task task);
        bool UpdateTask(Task task);
        bool DeleteTask(int id);
        bool Save();
    }
}

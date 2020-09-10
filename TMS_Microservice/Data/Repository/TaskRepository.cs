using System;
using System.Collections.Generic;
using System.Linq;
using TMS_Microservice.Data;
using TMS_Microservice.Data.Repository.IRepository;
using TMS_Microservice.Models;

namespace TMS_Microservice.Data.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _db;

        public TaskRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTask(Task task)
        {
            _db.Tasks.Add(task);
            return Save();
        }

        public bool DeleteTask(Task task)
        {
            _db.Remove(task);
            return Save();
        }

        public ICollection<Task> GetTasks()
        {
            return _db.Tasks.OrderBy(a => a.Id).ToList();
        }

        public Task GetTask(int taskId)
        {
            return _db.Tasks.FirstOrDefault(a => a.Id == taskId);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTask(Task task)
        {
            _db.Tasks.Update(task);
            return Save();
        }
    }
}

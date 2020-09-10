using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS_Microservice.Repository.IRepository;
using TMS_Microservice.Models;
using TMS_Microservice.Data;

namespace TMS_Microservice.Repository
{
    public class SubtaskRepository : ISubtaskRepository
    {

        private readonly ApplicationDbContext _db;

        public SubtaskRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateSubtask(Subtask subtask)
        {
            _db.Subtasks.Add(subtask);
            return Save();
        }

        public bool DeleteSubtask(int id)
        {
            Subtask subtask = GetSubtask(id);
            if (subtask == null)
            {
                return Save();
            }
            else
            {
                _db.Remove(subtask);
                return Save();
            }
            }

        

        public Subtask GetSubtask(int subtaskId)
        {
            return _db.Subtasks.FirstOrDefault(a => a.Id == subtaskId);
        }

        public ICollection<Subtask> GetSubtasks()
        {
            return _db.Subtasks.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateSubtask(Subtask subtask)
        {
            _db.Subtasks.Update(subtask);
            return Save();
        }

    }
}

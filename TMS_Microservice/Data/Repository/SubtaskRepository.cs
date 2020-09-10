﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS_Microservice.Data.Repository.IRepository;
using TMS_Microservice.Models;

namespace TMS_Microservice.Data.Repository
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

        public bool DeleteSubtask(Subtask subtask)
        {
            _db.Subtasks.Remove(subtask);
            return Save();
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

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
        private ITaskRepository _taskRepo;
        private readonly ApplicationDbContext _db;

        public SubtaskRepository(ApplicationDbContext db, ITaskRepository taskRepository)
        {
            _db = db;
            _taskRepo = taskRepository;
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
            var objList = GetSubtasksByTaskID(subtask.TaskId);
            if (objList.Count() > 0)
            {
                Models.Task task = _taskRepo.GetTask(subtask.TaskId);
                List<string> states = new List<string>();

                foreach (Subtask x in objList)
                {
                    //states.Append(x.State);
                    states.Add(x.State);
                }
                if (states.Contains("inProgress"))
                {
                    task.State = "inProgress";
                    _taskRepo.UpdateTask(task);
                    /*if (!_taskRepo.UpdateTask(task))
                    {
                        return StatusCode(500);
                    } */
                }
                else if (!states.Contains("inProgress") && !states.Contains("Planned"))
                {
                    task.State = "Completed";
                    _taskRepo.UpdateTask(task);
                    /* if (!_taskRepo.UpdateTask(task))
                     {
                         return StatusCode(500);
                     } */
                }
                else
                {
                    task.State = "Planned";
                    _taskRepo.UpdateTask(task);
                    /* if (!_taskRepo.UpdateTask(task))
                     {
                         return StatusCode(500);
                     } */
                }
            }
            return Save();
        }

        public ICollection<Subtask> GetSubtasksByTaskID(int taskid)
        {
            return _db.Subtasks.Where(a => a.TaskId == taskid).ToList();
        }

        public bool DeleteSubtask(int subtaskId)
        {
            Subtask subtask = GetSubtask(subtaskId);
            if (subtask == null)
            {
                return Save();
            }
            else
            {
                _db.Subtasks.Remove(subtask);
                return Save();
            }
        }

        public bool SubtaskExists(int subtaskId)
        {
            return _db.Subtasks.Any(a => a.Id == subtaskId);
        }

    }
}

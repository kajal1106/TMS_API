using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks = TMS_Microservice.Models.Task;

namespace TMS_Microservice.Controllers
{
    public class TaskTestController : ApiController
    {
        List<Tasks> tasks = new List<Tasks>();

        public TaskTestController() { }

        public TaskTestController(List<Tasks> tasks)
        {
            this.tasks = tasks;
        }

        public IEnumerable<Tasks> GetAllTasks()
        {
            return tasks;
        }

        public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
        {
            return await System.Threading.Tasks.Task.FromResult(GetAllTasks());
        }

        public IHttpActionResult GetTask(int id)
        {
            var task = tasks.FirstOrDefault((p) => p.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        public async Task<IHttpActionResult> GetTaskAsync(int id)
        {
            return await System.Threading.Tasks.Task.FromResult(GetTask(id));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Web.Http;
using TMS_Microservice.Models;

namespace TMS_Microservice.Controllers
{
    public class SubTaskTestController : ApiController
    {
        List<Subtask> tasks = new List<Subtask>();
        public SubTaskTestController() { }

        public SubTaskTestController(List<Subtask> tasks)
        {
            this.tasks = tasks;
        }

        public IEnumerable<Subtask> GetAllTasks()
        {
            return tasks;
        }

        public async Task<IEnumerable<Subtask>> GetAllTasksAsync()
        {
            return await System.Threading.Tasks.Task.FromResult(GetAllTasks());
        }

        public IHttpActionResult GetSubTask(int id)
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
            return await System.Threading.Tasks.Task.FromResult(GetSubTask(id));
        }
    }
}

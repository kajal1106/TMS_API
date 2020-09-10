using Microsoft.AspNetCore.Mvc;
using TMS_Microservice.Models;
using TMS_Microservice.Repository.IRepository;

namespace TMS_Microservice.Controllers
{
    [Route("api/[controller]")]
        [ApiController]
        public class TasksController : ControllerBase
        {

            private ITaskRepository _taskRepo;

            public TasksController(ITaskRepository taskRepository)
            {
                _taskRepo = taskRepository;
            }

            // GET: api/Tasks
            [HttpGet]
            public IActionResult GetTasks()
            {
                var objList = _taskRepo.GetTasks();
                return Ok(objList);
            }

            // GET: api/Tasks/5
            [HttpGet("{id:int}")]
            public IActionResult GetTask(int id)
            {
                var obj = _taskRepo.GetTask(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return Ok(obj);
            }

            // POST: api/Tasks
            [HttpPost]
            public IActionResult CreateTask([FromBody] Task task)
            {
                if (task == null)
                {
                    return BadRequest(ModelState);
                }

                if (!_taskRepo.CreateTask(task))
                {
                    return StatusCode(500);
                }

                return Ok();
            }

            // PUT: api/Tasks/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
            {
            }

            // DELETE: api/ApiWithActions/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
            }
        }
}

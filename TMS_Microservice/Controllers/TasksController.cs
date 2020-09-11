using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMS_Microservice.Models;
using TMS_Microservice.Repository.IRepository;

namespace TMS_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class TasksController : ControllerBase
    {

        private ITaskRepository _taskRepo;
        private ISubtaskRepository _subtaskRepo;


        public TasksController(ITaskRepository taskRepository, ISubtaskRepository subtaskRepository)
        {
            _taskRepo = taskRepository;
            _subtaskRepo = subtaskRepository;
        }

        /// <summary>
        /// Get list of all the tasks
        /// </summary>
           ///  <returns></returns>

    [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Task>))]
        public IActionResult GetTasks()
        {
            var objList = _taskRepo.GetTasks();
            var number = objList.Count();
            return Ok(objList);
        }

        /// <summary>
        /// Get individual task
        /// </summary>
        /// <param name = "taskId" > The Id of the task</param>
        /// <returns></returns>

        // GET: api/Tasks/5
            [HttpGet("{taskId:int}", Name ="GetTask")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetTask(int taskId)
        {
            var obj = _taskRepo.GetTask(taskId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }
        /// <summary>
        /// Add Tasks
        /// </summary>
        ///  <returns></returns>

        // POST: api/Tasks
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Task))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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

            return CreatedAtRoute("GetTask", new { taskId = task.Id }, task);
        }
        /// <summary>
        /// Generate CSV File
        /// </summary>
        ///  <returns></returns>

        [HttpGet("csvfile/{date:datetime}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetTasksByDate(DateTime date)
        {
            var obj = _taskRepo.GetTasksDate(date);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                var sb = new StringBuilder();

                sb.Append("Id,Name,Description,State,Start_Date,Finish_date\r\n");
                foreach (Task tasks in obj)
                {
                    sb.AppendFormat("\"{0}\",", tasks.Id);
                    sb.AppendFormat("\"{0}\",", tasks.Name);
                    sb.AppendFormat("\"{0}\",", tasks.Description);
                    sb.AppendFormat("\"{0}\",", tasks.State);
                    sb.AppendFormat("\"{0}\",", tasks.Start_date.ToShortDateString());
                    sb.AppendFormat("\"{0}\"\r\n", tasks.Finish_date.ToShortDateString());
                    //no comma for the last item, but a new line
                }
                return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Report.csv");
            }
            /*if (obj == null)
            {
                return NotFound();
            }*/

            //return Content(sb.ToString());
            //return Ok(obj); */
        }

        /// <summary>
        /// Update Tasks using ID
        /// </summary>
        ///  <returns></returns>

        // PUT: api/Tasks/5
        [HttpPut("{taskId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int taskId, [FromBody] Task task)
        {
            if (task == null)
            {
                return BadRequest(ModelState);
            }

            var objList = _subtaskRepo.GetSubtasksByTaskID(taskId);
            if (objList.Count() < 1)
            {
                if (!_taskRepo.UpdateTask(task))
                {
                    return StatusCode(500);
                }

            }
            else
            {

                List<string> states = new List<string>();
                foreach (Subtask x in objList)
                {
                    states.Add(x.State);
                }
                if (states.Contains("inProgress"))
                {
                    task.State = "inProgress";
                    if (!_taskRepo.UpdateTask(task))
                    {
                        return StatusCode(500);
                    }
                }
                else if (!states.Contains("inProgress") && !states.Contains("Planned"))
                {
                    task.State = "Completed";
                    if (!_taskRepo.UpdateTask(task))
                    {
                        return StatusCode(500);
                    }
                }
                else
                {
                    task.State = "Planned";
                    if (!_taskRepo.UpdateTask(task))
                    {
                        return StatusCode(500);
                    }
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Delete Tasks Using ID
        /// </summary>
        ///  <returns></returns>

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{taskId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int taskId)
        {
            if (!_taskRepo.TaskExists(taskId))
            {
                return NotFound();
            }

            if (!_taskRepo.DeleteTask(taskId))
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}

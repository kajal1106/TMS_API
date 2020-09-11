using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS_Microservice.Models;
using TMS_Microservice.Repository.IRepository;

namespace TMS_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtasksController : ControllerBase
    {

        private ISubtaskRepository _subtaskRepo;
        private ITaskRepository _taskRepo;

        public SubtasksController(ISubtaskRepository subtaskRepository, ITaskRepository taskRepository)
        {
            _subtaskRepo = subtaskRepository;
            _taskRepo = taskRepository;
        }

        
            //<summary>
            //Get a list of all the subtasks
            //</summary>
            //<returns></returns>

        // GET: api/Subtasks
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Subtask>))]
        public IActionResult GetSubtasks()
        {
            var objList = _subtaskRepo.GetSubtasks();
            return Ok(objList);
        }

            /// <summary>
            ///  Get individual subtask
            /// </summary>
            /// <param name = "subtaskId" > The Id of the subtask</param>
            /// <returns></returns>

        // GET: api/Subtasks/5
            [HttpGet("{subtaskId:int}", Name = "GetSubtask")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Subtask))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetSubtask(int subtaskId)
        {
            var obj = _subtaskRepo.GetSubtask(subtaskId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        // [Route("taskid/")]
        [HttpGet("taskid/{taskid:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Subtask))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetSubtasksbytaskid(int taskid)
        {
            var obj = _subtaskRepo.GetSubtasksByTaskID(taskid);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        // POST: api/Subtasks
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Subtask))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult CreateSubtask([FromBody] Subtask subtask)
        {
            if (subtask == null)
            {
                return BadRequest(ModelState);
            }

            if (!_subtaskRepo.CreateSubtask(subtask))
            {
                return StatusCode(500);
            }

            return CreatedAtRoute("GetSubtask", new { subtaskId = subtask.Id }, subtask);
        }

        // PUT: api/Subtasks/5
        [HttpPut("{subtaskId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Put(int subtaskId, [FromBody] Subtask subtask)
        {
            if (subtask == null)
            {
                return BadRequest(ModelState);
            }

            if (!_subtaskRepo.UpdateSubtask(subtask))
            {
                return StatusCode(500);
            }
            var objList = _subtaskRepo.GetSubtasksByTaskID(subtaskId);
            return Ok();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{subtaskId:int}")]
        public IActionResult Delete(int subtaskId)
        {
            if (!_subtaskRepo.SubtaskExists(subtaskId))
            {
                return NotFound();
            }

            if (!_subtaskRepo.DeleteSubtask(subtaskId))
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}

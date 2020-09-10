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

        public SubtasksController(ISubtaskRepository subtaskRepository)
        {
            _subtaskRepo = subtaskRepository;
        }

        // GET: api/Subtasks
        [HttpGet]
        public IActionResult GetSubtasks()
        {
            var objList = _subtaskRepo.GetSubtasks();
            return Ok(objList);
        }

        // GET: api/Subtasks/5
        [HttpGet("{id:int}")]
        public IActionResult GetSubtask(int id)
        {
            var obj = _subtaskRepo.GetSubtask(id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        // POST: api/Subtasks
        [HttpPost]
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

            return Ok();
        }

        // PUT: api/Subtasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Subtask subtask)
        {
            if (subtask == null)
            {
                return BadRequest(ModelState);
            }

            if (!_subtaskRepo.UpdateSubtask(subtask))
            {
                return StatusCode(500);
            }

            return Ok();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _subtaskRepo.DeleteSubtask(id);
            if (!obj)
            {
                return NotFound();
            }
            return Ok(obj);
        }
    }
}

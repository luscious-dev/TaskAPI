using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;
using TaskAPI.Models.DTOs;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private DatabaseContext _databaseContext;
        public TaskController(DatabaseContext dbContext) {
            _databaseContext = dbContext;
        }

        // GET : api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskAPI.Models.Task>>> GetAllTasks()
        {
            if(_databaseContext.Tasks is null)
            {
                return NotFound();
            }
            var tasks = await _databaseContext.Tasks.ToListAsync();
            return Ok(tasks);
        }

        // GET  : api/Task/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskAPI.Models.Task>> GetTask(int id)
        {
            if(_databaseContext is null)
            {
                return NotFound();
            }
            var task = await _databaseContext.Tasks.FindAsync(id);
            if(task is null)
            {
                return BadRequest("Task not found.");
            }
            return Ok(task);
        }

        // POST : api/Task
        [HttpPost]
        public async Task<ActionResult<TaskAPI.Models.Task>> CreateTask([FromBody] TaskCreateDto data)
        {
            if(_databaseContext is null)
            {
                return NotFound();
            }
            TaskAPI.Models.Task task = new TaskAPI.Models.Task
            {
                Title = data.Title,
                Description = data.Description,
                DueDate = data.DueDate,
                Priority = data.Priority,
                UserId = 1
            };
            _databaseContext.Tasks.Add(task);
            await _databaseContext.SaveChangesAsync();

            return Ok(task);
        }

        // PUT : api/Task/:id
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskAPI.Models.Task>> UpdateTask(int id, [FromBody] TaskUpdateDto data)
        {
            if(_databaseContext is null)
            {
                return NotFound();
            }

            var existingTask = await _databaseContext.Tasks.FindAsync(id);
            if(existingTask is null)
            {
                return NotFound("Task was not found.");
            }

            existingTask.Title = data.Title ?? existingTask.Title;
            existingTask.Description = data.Description ?? existingTask.Description;
            existingTask.DueDate = data.DueDate ?? existingTask.DueDate;
            existingTask.Status = data.Status ?? existingTask.Status;
            existingTask.Priority = data.Priority ?? existingTask.Priority;

            //_databaseContext.Tasks.Update(existingTask);
            await _databaseContext.SaveChangesAsync();

            return Ok(existingTask);

        }

        // DELETE : api/Task/:id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            if(_databaseContext.Tasks == null)
            {
                return NotFound();
            }
            var existingTask = await _databaseContext.Tasks.FindAsync(id);
            if(existingTask is null)
            {
                return NotFound("The task was not found");
            }
            _databaseContext.Tasks.Remove(existingTask);
            await _databaseContext.SaveChangesAsync();
            return NoContent();
        }
    }

}

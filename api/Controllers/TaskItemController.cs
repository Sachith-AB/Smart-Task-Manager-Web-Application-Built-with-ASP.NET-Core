using api.Data;
using api.DTOs.TaskItems;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/taskItems")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public TaskItemController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taskItems = await _context.TaskItem.ToListAsync();
            var taskItemDto = taskItems.Select(s => s.toTaskItemDto());
            return Ok(taskItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var taskItem = await _context.TaskItem.FindAsync(id);

            if (taskItem == null) return NotFound();

            return Ok(taskItem.toTaskItemDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskItemDto newItem)
        {
            var newTaskItem = newItem.toTaskItemFromCreateDto();
            await _context.TaskItem.AddAsync(newTaskItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = newTaskItem.Id }, newTaskItem.toTaskItemDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem([FromRoute] int Id, [FromBody] UpdateTaskItemDto updateTaskItemDto)
        {
            var TaskItemModel = await _context.TaskItem.FirstOrDefaultAsync(x => x.Id == Id);

            if (TaskItemModel == null)
            {
                return NotFound();
            }

            TaskItemModel.Description = updateTaskItemDto.Description;
            TaskItemModel.Piority = updateTaskItemDto.Piority;
            TaskItemModel.Status = updateTaskItemDto.Status;
            TaskItemModel.Deadline = updateTaskItemDto.Deadline;
            TaskItemModel.AssignToUserId = updateTaskItemDto.AssignToUserId;
            TaskItemModel.DueDate = updateTaskItemDto.DueDate;
            TaskItemModel.IsCompleted = updateTaskItemDto.IsCompleted;

            await _context.SaveChangesAsync();

            return Ok(TaskItemModel.toTaskItemDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem([FromRoute] int Id)
        {
            var TaskItemModel = await _context.TaskItem.FirstOrDefaultAsync(x => x.Id == Id);

            if (TaskItemModel == null)
            {
                return NotFound();
            }

            _context.TaskItem.Remove(TaskItemModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
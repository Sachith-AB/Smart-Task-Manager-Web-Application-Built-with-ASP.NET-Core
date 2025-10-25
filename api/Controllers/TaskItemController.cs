using api.Data;
using api.DTOs.TaskItems;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            var taskItems = _context.TaskItem.ToList().Select(s => s.toTaskItemDto());
            return Ok(taskItems);
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id)
        {
            var taskItem = _context.TaskItem.Find(id);

            if (taskItem == null) return NotFound();

            return Ok(taskItem.toTaskItemDto());
        }

        [HttpPost]
        public IActionResult createTaskItem([FromBody] CreateTaskItemDto newItem)
        {
            var newTaskItem = newItem.toTaskItemFromCreateDto();
            _context.TaskItem.Add(newTaskItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getById), new { id = newTaskItem.Id }, newTaskItem.toTaskItemDto());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTaskItem([FromRoute] int Id, [FromBody] UpdateTaskItemDto updateTaskItemDto)
        {
            var TaskItemModel = _context.TaskItem.FirstOrDefault(x => x.Id == Id);

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

            _context.SaveChanges();

            return Ok(TaskItemModel.toTaskItemDto());
        }
    }
}
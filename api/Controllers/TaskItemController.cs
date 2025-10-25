using api.DTOs.TaskItems;
using api.Mappers;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/taskItems")]
    [ApiController]
    public class TaskItemController(ITaskItemRepository repo) : ControllerBase
    {
        private readonly ITaskItemRepository _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taskItems = await _repo.GetAllAsync();
            var taskItemDto = taskItems.Select(s => s.toTaskItemDto());
            return Ok(taskItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var taskItem = await _repo.GetById(Id);

            if (taskItem == null) return NotFound();

            return Ok(taskItem.toTaskItemDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskItemDto newItem)
        {
            var newTaskItem = newItem.toTaskItemFromCreateDto();
            var createdItem = await _repo.CreateTaskItem(newTaskItem);
            return CreatedAtAction(nameof(GetById), new { id = newTaskItem.Id }, newTaskItem.toTaskItemDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem([FromRoute] int Id, [FromBody] UpdateTaskItemDto updateTaskItemDto)
        {
            var taskItemModel = updateTaskItemDto.toTaskItemFromUpdateDto();
            var updatedItem = await _repo.UpdateTaskItem(Id, taskItemModel);

            if (updatedItem == null)
            {
                return NotFound();
            }

            return Ok(updatedItem.toTaskItemDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem([FromRoute] int Id)
        {
            var deleted = await _repo.DeleteTaskItem(Id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
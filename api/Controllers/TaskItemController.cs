using System.Security.Claims;
using api.DTOs.TaskItems;
using api.Helpers;
using api.Mappers;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/taskItems")]
    [ApiController]
    [Authorize]
    public class TaskItemController(ITaskItemRepository repo) : ControllerBase
    {
        private readonly ITaskItemRepository _repo = repo;

        private string? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
            return userIdClaim?.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? sortBy, [FromQuery] bool isDescending = false, [FromQuery] string? searchTerm = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new QueryObject(sortBy, isDescending, searchTerm, pageNumber, pageSize);
            var taskItems = await _repo.GetAllAsync(query);
            var taskItemDto = taskItems.Select(s => s.toTaskItemDto());
            return Ok(taskItemDto);
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
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found in token" });
            }

            var newTaskItem = newItem.toTaskItemFromCreateDto();
            newTaskItem.AssignToUserId = userId;  // Assign task to current user
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
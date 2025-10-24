using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
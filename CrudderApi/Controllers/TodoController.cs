using Microsoft.AspNetCore.Mvc;
using CrudderApi.Models;
using Microsoft.AspNetCore.Authorization;
using CrudderApi.Services;
using CrudderApi.Extensions;
using CrudderApi.DTOs.Todo;
using AutoMapper;

namespace CrudderApi.Controllers
{
    [Route("api/todos")]
    [ApiController]
    [Authorize]
    public class TodoController(TodoService todoService, IMapper mapper) : ControllerBase
    {
        private readonly TodoService _todoService = todoService;
        private readonly IMapper _mapper = mapper;

        protected int UserId => User.GetUserId();

        [HttpGet] // api/todos
        public async Task<ActionResult<IEnumerable<TodoResponse>>> GetAll()
        {
            var todos = await _todoService.GetAllByUserAsync(UserId);

            var response = _mapper.Map<List<TodoResponse>>(todos);

            return Ok(response);
        }

        [HttpGet("{id}")] // api/todos/1
        public async Task<ActionResult<TodoResponse>> GetById(int id)
        {
            var todo = await _todoService.GetByIdAsync(id, UserId);

            if (todo == null) return NotFound();

            var response = _mapper.Map<TodoResponse>(todo);
            return Ok(response);
        }

        [HttpPost] // api/todos
        public async Task<ActionResult<TodoResponse>> Create(CreateTodoRequest request)
        {
            var todo = _mapper.Map<TodoItem>(request);
            todo.UserId = UserId;

            var created = await _todoService.CreateAsync(todo);
            var response = _mapper.Map<TodoResponse>(created);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }

        [HttpPut("{id}")] // api/todos/1
        public async Task<IActionResult> Update(int id, UpdateTodoRequest request)
        {
            var updated = await _todoService.UpdateAsync(id, UserId, request);
            if (updated == null) return NotFound();

            var response = _mapper.Map<TodoResponse>(updated);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _todoService.DeleteAsync(id, UserId);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
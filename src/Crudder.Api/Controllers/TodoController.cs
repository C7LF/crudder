using Crudder.Application.Todos.Commands.CreateTodo;
using Crudder.Application.Todos.Commands.UpdateTodo;
using Crudder.Application.Todos.Commands.DeleteTodo;
using Crudder.Application.Todos.Queries.GetTodoById;
using Crudder.Application.Todos.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Crudder.Application.Todos.Queries.GetAllTodos;
using System.Security.Claims;
using Crudder.Api.Dtos.Todo;

namespace Crudder.Api.Controllers;

[Route("api/todos")]
[ApiController]
[Authorize]
public class TodoController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // helper property to get current user
    protected int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    // GET: api/todos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll()
    {
        var todos = await _mediator.Send(new GetAllTodosQuery(UserId));
        return Ok(todos);
    }

    // GET: api/todos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetById(int id)
    {
        var todo = await _mediator.Send(new GetTodoByIdQuery(UserId, id));
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    // POST: api/todos
    [HttpPost]
    public async Task<ActionResult<TodoDto>> Create([FromBody] CreateTodoRequest request)
    {
        var todo = await _mediator.Send(new CreateTodoCommand(
            UserId,
            request.Title
        ));

        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    // PUT: api/todos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoRequest request)
    {
        var todo = await _mediator.Send(new UpdateTodoCommand(
            UserId,
            id,
            request.Title,
            request.Content,
            request.Completed,
            request.LabelIds
        ));

        if (todo == null) return NotFound();
        return Ok(todo);
    }

    // DELETE: api/todos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _mediator.Send(new DeleteTodoCommand(UserId, id));
        if (!deleted) return NotFound();
        return NoContent();
    }
}


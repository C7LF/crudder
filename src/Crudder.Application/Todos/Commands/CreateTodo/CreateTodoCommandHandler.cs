using Crudder.Application.Todos.Dtos;
using Crudder.Domain.Entities;
using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Todos.Commands.CreateTodo;

public class CreateTodoHandler(
    ITodoRepository todoRepository
) : IRequestHandler<CreateTodoCommand, TodoDto>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new TodoItem
        {
            Title = request.Title,
            UserId = request.UserId
        };

        await _todoRepository.AddAsync(todo);

        return new TodoDto(todo.Id, todo.Title, todo.Content, false, []);
    }
}

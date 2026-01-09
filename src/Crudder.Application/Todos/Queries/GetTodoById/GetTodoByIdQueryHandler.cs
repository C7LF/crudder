using Crudder.Application.Labels.Dtos;
using Crudder.Application.Todos.Dtos;
using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Todos.Queries.GetTodoById;

public class GetTodoByIdHandler(ITodoRepository todoRepository)
    : IRequestHandler<GetTodoByIdQuery, TodoDto?>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<TodoDto?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.TodoId, request.UserId);

        if (todo == null) return null;

        return new TodoDto(
            todo.Id,
            todo.Title,
            todo.Content,
            todo.Completed,
            [.. todo.Labels.Select(label => new LabelDto(
                label.Id,
                label.Text,
                label.Colour
            ))]
        );
    }
}

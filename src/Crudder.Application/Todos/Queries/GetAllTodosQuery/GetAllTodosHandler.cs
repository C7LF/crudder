using Crudder.Application.Labels.Dtos;
using Crudder.Application.Todos.Dtos;
using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Todos.Queries.GetAllTodos;

public class GetAllTodosHandler(ITodoRepository todoRepository)
    : IRequestHandler<GetAllTodosQuery, List<TodoDto>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<List<TodoDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        var todos = await _todoRepository.GetAllByUserAsync(request.UserId);

        return [.. todos.Select(todo => new TodoDto(
            todo.Id,
            todo.Title,
            todo.Content,
            todo.Completed,
            [.. todo.Labels.Select(label => new LabelDto(
                label.Id,
                label.Text,
                label.Colour
            ))]
        ))];

    }
}

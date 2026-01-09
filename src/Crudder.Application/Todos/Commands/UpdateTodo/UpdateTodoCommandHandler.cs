using Crudder.Application.Todos.Dtos;
using Crudder.Application.Labels.Dtos;
using Crudder.Domain.Entities;
using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Todos.Commands.UpdateTodo;

public class UpdateTodoHandler(
    ITodoRepository todoRepository,
    ILabelRepository labelRepository
) : IRequestHandler<UpdateTodoCommand, TodoDto?>
{
    private readonly ITodoRepository _todoRepository = todoRepository;
    private readonly ILabelRepository _labelRepository = labelRepository;

    public async Task<TodoDto?> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.TodoId, request.UserId);
        if (todo == null) return null;

        if (!string.IsNullOrEmpty(request.Title)) todo.Title = request.Title;
        if (request.Content != null) todo.Content = request.Content;

        if (request.Completed.HasValue) todo.Completed = request.Completed.Value;

        if (request.LabelIds != null)
        {
            var labels = await _labelRepository.GetByIdsAsync(request.LabelIds, request.UserId);
            todo.Labels = labels;
        }

        await _todoRepository.UpdateAsync(todo);

        return new TodoDto(todo.Id, todo.Title, todo.Content, todo.Completed, [
            .. todo.Labels.Select(label => new LabelDto(
                label.Id,
                label.Text,
                label.Colour
            ))
        ]);
    }
}

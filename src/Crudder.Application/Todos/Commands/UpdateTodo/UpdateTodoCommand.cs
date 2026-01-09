using Crudder.Application.Todos.Dtos;
using MediatR;

namespace Crudder.Application.Todos.Commands.UpdateTodo;

public record UpdateTodoCommand(
    int UserId,
    int TodoId,
    string? Title,
    string? Content,
    bool? Completed,
    List<int>? LabelIds
) : IRequest<TodoDto?>;

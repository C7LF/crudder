using Crudder.Application.Todos.Dtos;
using MediatR;

namespace Crudder.Application.Todos.Commands.CreateTodo;

public record CreateTodoCommand(
    int UserId,
    string Title
) : IRequest<TodoDto>;

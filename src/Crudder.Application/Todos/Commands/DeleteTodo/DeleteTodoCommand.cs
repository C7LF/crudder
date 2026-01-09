using MediatR;

namespace Crudder.Application.Todos.Commands.DeleteTodo;

public record DeleteTodoCommand(int UserId, int TodoId) : IRequest<bool>;

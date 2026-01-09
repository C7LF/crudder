using Crudder.Application.Todos.Dtos;
using MediatR;

namespace Crudder.Application.Todos.Queries.GetTodoById;

public record GetTodoByIdQuery(int UserId, int TodoId) : IRequest<TodoDto?>;

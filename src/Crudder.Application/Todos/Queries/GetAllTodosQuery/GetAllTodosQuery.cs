using Crudder.Application.Todos.Dtos;
using MediatR;

namespace Crudder.Application.Todos.Queries.GetAllTodos;

public record GetAllTodosQuery(int UserId) : IRequest<List<TodoDto>>;


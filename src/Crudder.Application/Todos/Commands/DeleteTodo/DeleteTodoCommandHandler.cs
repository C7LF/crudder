using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Todos.Commands.DeleteTodo;

public class DeleteTodoHandler(ITodoRepository todoRepository) : IRequestHandler<DeleteTodoCommand, bool>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.TodoId, request.UserId);
        if (todo == null) return false;

        await _todoRepository.DeleteAsync(todo);
        return true;
    }
}

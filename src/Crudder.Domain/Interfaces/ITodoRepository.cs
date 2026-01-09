using Crudder.Domain.Entities;

namespace Crudder.Domain.Interfaces;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllByUserAsync(int userId);
    Task<TodoItem?> GetByIdAsync(int id, int userId);
    Task AddAsync(TodoItem todo);
    Task UpdateAsync(TodoItem todo);
    Task DeleteAsync(TodoItem todo);
}

using Crudder.Domain.Entities;
using Crudder.Domain.Interfaces;
using Crudder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crudder.Infrastructure.Repositories;

public class TodoRepository(CrudderDbContext context) : ITodoRepository
{
    private readonly CrudderDbContext _context = context;

    public async Task<List<TodoItem>> GetAllByUserAsync(int userId)
    {
        return await _context.TodoItems
            .Include(t => t.Labels) // many-to-many
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id, int userId)
    {
        return await _context.TodoItems
            .Include(t => t.Labels)
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }

    public async Task AddAsync(TodoItem todo)
    {
        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem todo)
    {
        _context.TodoItems.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoItem todo)
    {
        _context.TodoItems.Remove(todo);
        await _context.SaveChangesAsync();
    }
}

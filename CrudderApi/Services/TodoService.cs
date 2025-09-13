using AutoMapper;
using CrudderApi.Data;
using CrudderApi.DTOs.Todo;
using CrudderApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudderApi.Services
{
    public class TodoService(TodoContext context, IMapper mapper, ILogger<TodoService> logger)
    {
        private readonly TodoContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<TodoService> _logger = logger;

        public async Task<List<TodoItem>> GetAllByUserAsync(int userId)
        {
            _logger.LogInformation("Fetching all todos for UserId {UserId}", userId);
            return await _context.TodoItems.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id, int userId)
        {
            _logger.LogInformation("Fetching todo {TodoId} for UserId {UserId}", id, userId);
            return await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        // Create new todo
        public async Task<TodoItem> CreateAsync(TodoItem todo)
        {
            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created todo {TodoId} for UserId {UserId}", todo.Id, todo.UserId);
            return todo;
        }

        // Update todo
        public async Task<TodoItem?> UpdateAsync(int id, int userId, UpdateTodoRequest request)
        {
            var existing = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (existing == null) return null;

            // Map changes from DTO → existing entity
            _mapper.Map(request, existing);

            await _context.SaveChangesAsync();
            return existing;
        }

        // Delete todo
        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var todo = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (todo == null) return false;

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    };
};
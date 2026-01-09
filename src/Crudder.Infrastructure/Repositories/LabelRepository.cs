using Crudder.Domain.Entities;
using Crudder.Domain.Interfaces;
using Crudder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crudder.Infrastructure.Repositories;

public class LabelRepository(CrudderDbContext context) : ILabelRepository
{
    private readonly CrudderDbContext _context = context;

    public async Task<List<Label>> GetAllByUserAsync(int userId)
        => await _context.Labels
            .Where(l => l.UserId == userId)
            .ToListAsync();

    public async Task<int> CountByUserAsync(int userId)
        => await _context.Labels.CountAsync(l => l.UserId == userId);

    public async Task AddAsync(Label label)
    {
        _context.Labels.Add(label);
        await _context.SaveChangesAsync();
    }

    public async Task<Label?> GetByIdAsync(int id, int userId)
        => await _context.Labels
            .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

    public async Task DeleteAsync(Label label)
    {
        _context.Labels.Remove(label);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Label>> GetByIdsAsync(List<int> ids, int userId)
        => await _context.Labels
            .Where(l => ids.Contains(l.Id) && l.UserId == userId)
            .ToListAsync();
}

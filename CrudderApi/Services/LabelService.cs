using CrudderApi.Data;
using CrudderApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudderApi.Services
{
    public class LabelService(TodoContext context, ILogger<LabelService> logger)
    {
        private readonly TodoContext _context = context;
        private readonly ILogger<LabelService> _logger = logger;

        public async Task<List<Label>> GetAllByUserAsync(int userId)
        {
            _logger.LogInformation("Retrieving all labels for {UserId}", userId);
            var allUserLabels = await _context.Labels.Where(l => l.UserId == userId).ToListAsync();
            return allUserLabels;
        }

        public async Task<Label> CreateAsync(Label label)
        {
            var currentCount = await _context.Labels.CountAsync(l => l.UserId == label.UserId);

            const int maxLabels = 20;

            if (currentCount >= maxLabels)
            {
                throw new InvalidOperationException($"Cannot have more than {maxLabels} labels.");
            }

            await _context.AddAsync(label);
            await _context.SaveChangesAsync();

            return label;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var label = await _context.Labels.FirstOrDefaultAsync(label => label.Id == id && label.UserId == userId);

            if (label == null) return false;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
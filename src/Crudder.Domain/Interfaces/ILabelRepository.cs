using Crudder.Domain.Entities;

namespace Crudder.Domain.Interfaces;

public interface ILabelRepository
{
    Task<List<Label>> GetAllByUserAsync(int userId);
    Task<int> CountByUserAsync(int userId);
    Task AddAsync(Label label);
    Task<Label?> GetByIdAsync(int id, int userId);
    Task DeleteAsync(Label label);
    Task<List<Label>> GetByIdsAsync(List<int> ids, int userId);
}
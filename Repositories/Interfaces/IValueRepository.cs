using InfotecsExperiment.Entity;

namespace InfotecsExperiment.Repository.Interfaces;

public interface IValueRepository
{
    Task<List<Value>> GetAllByFileTitleAsync(string title);
    Task<Value> AddAsync(Value value);
    Task AddCollectionAsync(IEnumerable<Value> values);
    Task DeleteAllByFileTitle(string title);
}
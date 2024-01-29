using InfotecsExperiment.Entity;

namespace InfotecsExperiment.Repository.Interfaces;

public interface IResultRepository
{
    Task<Result> FindByFileTitleAsync(string title);
    Task<Result> AddAsync(Result result);
    Task<Result> UpdateAsync(Result result);
    Task<Result> DeleteAsync(Result result);
}
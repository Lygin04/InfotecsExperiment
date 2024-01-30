using System.Linq.Expressions;
using InfotecsExperiment.Entity;

namespace InfotecsExperiment.Repository.Interfaces;

public interface IResultRepository
{
    Task<Result> FindByFileTitleAsync(string title);
    Task<List<Result>> WhereAsync(Expression<Func<Result, bool>> where);
    Task<Result> AddAsync(Result result);
    Task<Result> UpdateAsync(Result result);
    Task<Result> DeleteAsync(Result result);
}
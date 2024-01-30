using System.Linq.Expressions;
using InfotecsExperiment.Entity;
using InfotecsExperiment.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfotecsExperiment.Repository;

public class ResultRepository : IResultRepository
{
    private readonly DataContext _context;

    public ResultRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<Result>> WhereAsync(Expression<Func<Result, bool>> where)
    {
        return await _context.Results.Where(where).ToListAsync();
    }

    public async Task<Result> FindByFileTitleAsync(string title)
    {
        return await _context.Results.FirstOrDefaultAsync(result => result.File.Name == title)
               ?? throw new InvalidOperationException();
    }

    public async Task<Result> AddAsync(Result result)
    {
        var res = (await _context.AddAsync(result)).Entity;
        await _context.SaveChangesAsync();
        return res;
    }

    public async Task<Result> UpdateAsync(Result result)
    {
        _context.Results.Update(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<Result> DeleteAsync(Result result)
    {
        _context.Results.Remove(result);
        await _context.SaveChangesAsync();
        return result;
    }
}
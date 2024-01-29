using InfotecsExperiment.Entity;
using InfotecsExperiment.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfotecsExperiment.Repository;

public class ValueRepository : IValueRepository
{
    private readonly DataContext _context;

    public ValueRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Value>> GetAllByFileTitleAsync(string title)
    {
        return await _context.Values.Where(value => value.File != null && value.File.Name == title).ToListAsync();
    }

    public async Task<Value> AddAsync(Value value)
    {
        var result = (await _context.Values.AddAsync(value)).Entity;
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task AddCollectionAsync(IEnumerable<Value> values)
    {
        await _context.Values.AddRangeAsync(values);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAllByFileTitle(string title)
    {
        var values = await _context.Values.Where(value => value.File != null && value.File.Name == title)
            .ToListAsync();
        _context.Values.RemoveRange(values);
        await _context.SaveChangesAsync();
    }
}
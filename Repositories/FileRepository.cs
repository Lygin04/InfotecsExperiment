using InfotecsExperiment.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using File = InfotecsExperiment.Entity.File;

namespace InfotecsExperiment.Repository;

public class FileRepository : IFileRepository
{
    private readonly DataContext _context;

    public FileRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<File?> GetByIdAsync(int id)
    {
        return await _context.Files.FindAsync(id);
    }

    public async Task<File?> GetByNameAsync(string title)
    {
        return await _context.Files.FirstOrDefaultAsync(file => file.Name == title);
    }

    public async Task<File?> AddAsync(File file)
    {
        var result = (await _context.Files.AddAsync(file)).Entity;
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<File> UpdateAsync(File file)
    {
        _context.Files.Update(file);
        await _context.SaveChangesAsync();
        return file;
    }

    public async Task<File> DeleteAsync(File file)
    {
        var result = _context.Remove(file).Entity;
        await _context.SaveChangesAsync();
        return result;
    }
}
using File = InfotecsExperiment.Entity.File;

namespace InfotecsExperiment.Repository.Interfaces;

public interface IFileRepository
{
    Task<File?> GetByIdAsync(int id);
    Task<File?> GetByNameAsync(string title);
    Task<File?> AddAsync(File file);
    Task<File> UpdateAsync(File file);
    Task<File> DeleteAsync(File file);
}
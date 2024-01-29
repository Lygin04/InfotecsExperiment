using InfotecsExperiment.Entity;

namespace InfotecsExperiment.Services.Interfaces;

public interface IValueService
{
    Task<List<Value>> GetAllByFileTitleAsync(string name);
    Task<Result> CalculateResultAsync(List<Value> values);
}
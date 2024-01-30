using InfotecsExperiment.Dto;
using InfotecsExperiment.Entity;

namespace InfotecsExperiment.Services.Interfaces;

public interface IResultService
{
    Task<ResultDto> GetByFileNameAsync(string name);
    Task<List<ResultDto>> GetByAverageScoreInRangeAsync(double? min, double? max);
    Task<List<ResultDto>> GetByAverageTimeInRangeAsync(double? min, double? max);
}
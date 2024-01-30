using InfotecsExperiment.Dto;
using InfotecsExperiment.Entity;
using InfotecsExperiment.Repository;
using InfotecsExperiment.Repository.Interfaces;
using InfotecsExperiment.Services.Interfaces;

namespace InfotecsExperiment.Services;

public class ResultService : IResultService
{
    private readonly IResultRepository _resultRepository;

    public ResultService(IResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public async Task<List<ResultDto>> GetByAverageScoreInRangeAsync(double? min, double? max)
    {
        var results = await _resultRepository.WhereAsync
            (result => result.AvgScore <= max && result.AvgScore >= min);
        return results.Select(result => new ResultDto
        {
            Id = result.Id,
            FirstExperimentDate = result.FirstExperimentDate,
            LastExperimentDate = result.LastExperimentDate,
            MaxTimeExperiment = result.MaxTimeExperiment,
            MinTimeExperiment = result.MinTimeExperiment,
            AvgTimeExperiment = result.AvgTimeExperiment,
            Median = result.Median,
            MaxScore = result.MaxScore,
            MinScore = result.MinScore,
            CountExperiment = result.CountExperiment,
            FileId = result.FileId
        }).ToList();
    }

    public async Task<List<ResultDto>> GetByAverageTimeInRangeAsync(double? min, double? max)
    {
        var results = await _resultRepository.WhereAsync
            (result => result.AvgTimeExperiment <= max && result.AvgTimeExperiment >= min);
        return results.Select(result => new ResultDto
        {
            Id = result.Id,
            FirstExperimentDate = result.FirstExperimentDate,
            LastExperimentDate = result.LastExperimentDate,
            MaxTimeExperiment = result.MaxTimeExperiment,
            MinTimeExperiment = result.MinTimeExperiment,
            AvgTimeExperiment = result.AvgTimeExperiment,
            Median = result.Median,
            MaxScore = result.MaxScore,
            MinScore = result.MinScore,
            CountExperiment = result.CountExperiment,
            FileId = result.FileId
        }).ToList();
    }


    public async Task<ResultDto> GetByFileNameAsync(string name)
    {
        var result = await _resultRepository.FindByFileTitleAsync(name);
        ResultDto resultDto = new ResultDto
        {
            Id = result.Id,
            FirstExperimentDate = result.FirstExperimentDate,
            LastExperimentDate = result.LastExperimentDate,
            MaxTimeExperiment = result.MaxTimeExperiment,
            MinTimeExperiment = result.MinTimeExperiment,
            AvgTimeExperiment = result.AvgTimeExperiment,
            Median = result.Median,
            MaxScore = result.MaxScore,
            MinScore = result.MinScore,
            CountExperiment = result.CountExperiment,
            FileId = result.FileId
        };
        return resultDto;
    }
}
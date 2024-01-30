using InfotecsExperiment.Dto;
using InfotecsExperiment.Entity;
using InfotecsExperiment.Repository;
using InfotecsExperiment.Repository.Interfaces;
using InfotecsExperiment.Services.Interfaces;

namespace InfotecsExperiment.Services;

public class ValueService : IValueService
{
    private readonly IValueRepository _valueRepository;
    private readonly IResultRepository _resultRepository;

    public ValueService(IValueRepository valueRepository, IResultRepository resultRepository)
    {
        _valueRepository = valueRepository;
        _resultRepository = resultRepository;
    }

    public async Task<List<ValueDto>> GetAllByFileTitleAsync(string name)
    {
        var values = await _valueRepository.GetAllByFileTitleAsync(name);
        return values.Select(value => new ValueDto
        {
            Id = value.Id,
            StartDate = value.StartDate,
            Time = value.Time,
            Score = value.Score
        }).ToList();
    }
}
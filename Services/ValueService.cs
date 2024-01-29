using InfotecsExperiment.Entity;
using InfotecsExperiment.Repository;
using InfotecsExperiment.Services.Interfaces;

namespace InfotecsExperiment.Services;

public class ValueService : IValueService
{
    private readonly ValueRepository _valueRepository;
    private readonly ResultRepository _resultRepository;

    public ValueService(ValueRepository valueRepository, ResultRepository resultRepository)
    {
        _valueRepository = valueRepository;
        _resultRepository = resultRepository;
    }

    public async Task<List<Value>> GetAllByFileTitleAsync(string name)
    {
        var values = await _valueRepository.GetAllByFileTitleAsync(name);
        await CalculateResultAsync(values);
        return values;
    }

    public async Task<Result> CalculateResultAsync(List<Value> values)
    {
        return new Result();
    }
}
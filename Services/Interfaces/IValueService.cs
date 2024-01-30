using InfotecsExperiment.Dto;

namespace InfotecsExperiment.Services.Interfaces;

public interface IValueService
{
    Task<List<ValueDto>> GetAllByFileTitleAsync(string name);
}
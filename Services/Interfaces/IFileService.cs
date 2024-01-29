using InfotecsExperiment.Dto;

namespace InfotecsExperiment.Services.Interfaces;

public interface IFileService
{
    Task<string> UploadAsync(IFormFile formFile);
}
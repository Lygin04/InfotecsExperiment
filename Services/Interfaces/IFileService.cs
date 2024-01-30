using InfotecsExperiment.Dto;

namespace InfotecsExperiment.Services.Interfaces;

public interface IFileService
{
    Task<FileDto> UploadAsync(IFormFile formFile);
}
using System.Globalization;
using InfotecsExperiment.Dto;
using InfotecsExperiment.Entity;
using InfotecsExperiment.Repository;
using InfotecsExperiment.Services.Interfaces;
using File = InfotecsExperiment.Entity.File;

namespace InfotecsExperiment.Services;

public class FileService : IFileService
{
    private readonly FileRepository _fileRepository;
    private readonly ValueRepository _valueRepository;

    public FileService(FileRepository fileRepository, ValueRepository valueRepository)
    {
        _fileRepository = fileRepository;
        _valueRepository = valueRepository;
    }

    public async Task<string> UploadAsync(IFormFile formFile)
    {
        using var streamReader = new StreamReader(formFile.OpenReadStream());
        var lineNumber = 0;

        while (await streamReader.ReadLineAsync() is { } line)
        {
            lineNumber++;

            var components = line.Split(';');

            if (components.Length != 3 ||
                !DateTime.TryParseExact(components[0], "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out _) ||
                !int.TryParse(components[1], out int timeInSeconds) || timeInSeconds < 0 ||
                !double.TryParse(components[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double indicator) ||
                indicator < 0)
            {
                continue;
            }

            var data = new Value
            {
                StartDate = DateTime.ParseExact(components[0], "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture),
                Time = timeInSeconds,
                Score = indicator
            };

            await _valueRepository.AddAsync(data);
        }

        if (lineNumber is < 1 or > 10000)
        {
            return "Invalid number of lines in the file";
        }

        var fileInfo = new File
        {
            Author = formFile.Name,
            CreationDate = DateTime.Now
        };

        await _fileRepository.AddAsync(fileInfo);

        return $"File {fileInfo.Author} uploaded successfully";
    }
}
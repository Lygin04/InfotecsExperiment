using System.Globalization;
using InfotecsExperiment.Dto;
using InfotecsExperiment.Entity;
using InfotecsExperiment.Repository.Interfaces;
using InfotecsExperiment.Services.Interfaces;
using File = InfotecsExperiment.Entity.File;

namespace InfotecsExperiment.Services;

public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;
    private readonly IValueRepository _valueRepository;
    private readonly IResultRepository _resultRepository;
    public FileService(IFileRepository fileRepository, IValueRepository valueRepository, IResultRepository resultRepository)
    {
        _fileRepository = fileRepository;
        _valueRepository = valueRepository;
        _resultRepository = resultRepository;
    }

    public async Task<FileDto> UploadAsync(IFormFile formFile)
    {
        List<Value> values = new List<Value>();
        var fileInfo = new File
        {
            Name = formFile.FileName,
            CreationDate = DateTime.Now
        };
        File? file = await _fileRepository.AddAsync(fileInfo);
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
                Score = indicator,
                File = file,
            };
            values.Add(data);
        }
        
        if (lineNumber is < 1 or > 10000)
        {
            throw new Exception($"invalid values ({lineNumber} lines)");
        }

        foreach (var value in values)
        {
            await _valueRepository.AddAsync(value);
        }
        
        await CalculateResultAsync(values, file ?? throw new InvalidOperationException());
        return new FileDto
        {
            Id = file.Id,
            Name = file.Name,
            CreationDate = file.CreationDate
        };
    }
    
    public async Task CalculateResultAsync(List<Value> values, File file)
    {
        Result result = new Result();

        if (values.Any())
        {
            result.FirstExperimentDate = values.Min(v => v.StartDate);
            result.LastExperimentDate = values.Max(v => v.StartDate);
            result.MaxTimeExperiment = values.Max(v => v.Time);
            result.MinTimeExperiment = values.Min(v => v.Time);
            result.AvgTimeExperiment = (int)values.Average(v => v.Time);
            result.MaxScore = values.Max(v => v.Score);
            result.MinScore = values.Min(v => v.Score);
            result.CountExperiment = values.Count;
            result.FileId = values.First().File?.Id ?? 0;
            result.Median = (result.MaxScore + 1) / 2;
            result.File = file;
            
            await _resultRepository.AddAsync(result);
        }
    }
}
using InfotecsExperiment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsExperiment.Controllers;

[Route("[controller]")]
[ApiController]
public class ScienceController : Controller
{
    private readonly IFileService _fileService;
    private readonly IResultService _resultService;
    private readonly IValueService _valueService;

    public ScienceController(IFileService fileService, IResultService resultService, IValueService valueService)
    {
        _fileService = fileService;
        _resultService = resultService;
        _valueService = valueService;
    }

    [HttpPost("files")]
    public async Task<IActionResult> UploadFile(IFormFile formFile)
    {
        try
        {
            return Ok(await _fileService.UploadAsync(formFile));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("results")]
    public async Task<IActionResult> GetResults([FromQuery] string? fileName, 
        [FromQuery] double? minAverageIndicator, 
        [FromQuery] double? maxAverageIndicator, 
        [FromQuery] double? minAverageExecutionTime, 
        [FromQuery] double? maxAverageExecutionTime)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            return Ok(await _resultService.GetByFileNameAsync(fileName));   
        }

        if (maxAverageIndicator != null && minAverageIndicator != null)
        {
            return Ok(await _resultService.GetByAverageScoreInRangeAsync(minAverageIndicator, maxAverageIndicator));   
        }

        if (minAverageExecutionTime != null && maxAverageExecutionTime != null)
        {
            return Ok(await _resultService.GetByAverageTimeInRangeAsync(minAverageExecutionTime, maxAverageExecutionTime));   
        }

        return BadRequest("Invalid request parameters");
    }

    [HttpGet("values/{fileName}")]
    public async Task<IActionResult> GetValuesByName(string fileName)
    {
        return Ok(await _valueService.GetAllByFileTitleAsync(fileName));
    }
}
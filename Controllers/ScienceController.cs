using InfotecsExperiment.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsExperiment.Controllers;

[Route("[controller]")]
[ApiController]
public class ScienceController : Controller
{
    private readonly FileService _fileService;
    private readonly ResultService _resultService;
    private readonly ValueService _valueService;

    public ScienceController(FileService fileService, ResultService resultService, ValueService valueService)
    {
        _fileService = fileService;
        _resultService = resultService;
        _valueService = valueService;
    }

    [HttpPost("files")]
    public async Task<IActionResult> UploadFile(IFormFile formFile)
    {
        return Ok(_fileService.UploadAsync(formFile));
    }

    [HttpGet("results")]
    public async Task<IActionResult> GetResults()
    {
        return Ok();
    }

    [HttpGet("values/{fileName}")]
    public async Task<IActionResult> GetValuesByName(string fileName)
    {
        return Ok(_valueService.GetAllByFileTitleAsync(fileName));
    }
}
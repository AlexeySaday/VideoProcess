using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VideoProcess.Sample.Controllers.Dtos;
using VideoProcess.Sample.Domain;
using YandexDisk.Client;
using YandexDisk.Client.Clients;

namespace VideoProcess.Sample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{ 
    private readonly ILogger<UploadController> _logger;
    private readonly YandexDiskSettings _settings;
    private readonly IDiskApi _diskApi;

    public UploadController(ILogger<UploadController> logger, 
        IDiskApi diskApi, IOptions<YandexDiskSettings> settings)
    {
        _logger = logger;
        _diskApi = diskApi;
        _settings = settings.Value;
    }

    [HttpPost]
    public async Task<UploadResultDto> Post(IFormFile file)
    {
        try
        {
            await using var stream = file.OpenReadStream();
            var path = _settings.FilePath + '/' + GetPostfix + '_' + file.FileName;
            await _diskApi.Files.UploadFileAsync(path, true, stream);
            return new UploadResultDto
            {
                Url = path,
            };
        }
        catch (Exception e)
        { 
            _logger.LogError("Can`t upload the file: {Message}", e.Message);
            throw;
        }
    }

    private string GetPostfix => Guid.NewGuid().ToString();
}

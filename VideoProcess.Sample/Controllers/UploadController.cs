using Microsoft.AspNetCore.Mvc;
using VideoProcess.Sample.Controllers.Dtos;
using YandexDisk.Client;
using YandexDisk.Client.Clients;

namespace VideoProcess.Sample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{ 
    private readonly ILogger<UploadController> _logger;
    private readonly IDiskApi _diskApi;

    public UploadController(ILogger<UploadController> logger, IDiskApi diskApi)
    {
        _logger = logger;
        _diskApi = diskApi;
    }

    [HttpPost]
    public async Task<UploadResultDto> Post(IFormFile file)
    {
        try
        {
            await using var stream = file.OpenReadStream();
            var filename = file.ContentType.ToLower() + '_' + Guid.NewGuid().ToString() + "." + file.ContentType.Split('/').Last();
            await _diskApi.Files.UploadFileAsync(filename, true, stream);
            return new UploadResultDto
            {
                Url = filename,
            };
        }
        catch (Exception e)
        {
            _logger.LogError("Can`t upload the file: {Message}", e.Message);
            throw;
        }
    }
}

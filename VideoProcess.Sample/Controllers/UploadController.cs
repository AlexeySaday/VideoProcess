using Microsoft.AspNetCore.Mvc;
using VideoProcess.Sample.Controllers.Dtos;
using YandexDisk.Client;
using YandexDisk.Client.Clients;

namespace VideoProcess.Sample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    // https://t.me/AlexeyKildyushev#access_token=y0_AgAAAAA2qobvAAoWQAAAAADmKT9O_qckcntDTBeWmAeu1ZCP8Ha4DD4&token_type=bearer&expires_in=31536000
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
            var res = await _diskApi.Files.UploadFileAsync()
            awai
            return new UploadResultDto
            {
                Url = res,
            };
        }
        catch (Exception e)
        {
            _logger.LogError("Can`t upload the file: {Message}", e.Message);
            throw;
        }
    }
}

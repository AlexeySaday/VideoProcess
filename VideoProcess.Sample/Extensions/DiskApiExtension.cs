using Microsoft.Extensions.Options;
using VideoProcess.Sample.Domain;
using YandexDisk.Client; 
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace VideoProcess.Sample.Extensions;

public static class DiskApiExtension
{
    public static IServiceCollection AddDiskApi(this IServiceCollection services, 
        IConfigurationSection section)
    {
        return services
            .Configure<YandexDiskSettings>(section.Bind)
            .AddSingleton<IDiskApi, DiskHttpApi>(opt =>
            {
                var settings = opt.GetRequiredService<IOptions<YandexDiskSettings>>(); 
                var res = new DiskHttpApi(settings.Value.TokenId); 
                return res;
            });
    }
}
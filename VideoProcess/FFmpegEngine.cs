using System.Diagnostics;
using VideoProcess.NET.Domain;
using VideoProcess.NET.Extensions;

namespace VideoProcess.NET;

public class FfmpegEngine
{
    private readonly string _ffmpegPath; 

    public FfmpegEngine(string? ffmpegPath = null)
    {
        ffmpegPath ??= Constants.FfmpegExecutable;
        _ffmpegPath = ffmpegPath.TryGetFfmpegPath();
    }

    private async Task ExecuteAsync(FfmpegParameter parameters, CancellationToken cancellationToken)
    {
        var ffmpegProcess = CreateProcess(parameters);
        await ffmpegProcess.ExecuteAsync(cancellationToken).ConfigureAwait(false); 
    }

    //public async Task ExecuteAsync(string arguments, CancellationToken cancellationToken)
    //{
    //    var parameters = new FfmpegParameter
    //    {
    //        CustomArguments = arguments,

    //    };
    //    await ExecuteAsync(parameters, cancellationToken).ConfigureAwait(false);
    //}

    private FfmpegProcess CreateProcess(FfmpegParameter parameters)
    {
        var ffmpegProcess = new FfmpegProcess(parameters, _ffmpegPath);
        return ffmpegProcess;
    }  
}

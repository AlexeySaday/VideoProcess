using System.Diagnostics;
using VideoProcess.NET.Domain;
using VideoProcess.NET.Extensions;
using VideoProcess.NET.Input;

namespace VideoProcess.NET;

public class FfmpegEngine
{
    private readonly string _ffmpegPath; 

    public FfmpegEngine(string? ffmpegPath = null)
    {
        ffmpegPath ??= Constants.FfmpegExecutable;
        _ffmpegPath = ffmpegPath.TryGetFfmpegPath();
    }

    //public async Task<Metadata> GetMetaDataAsync(IInputArgument mediaFile, CancellationToken cancellationToken)
    //{
    //    var parameters = new FfmpegParameter
    //    {
    //        InputArgument = mediaFile,
    //        TaskType = FFmpegTaskType.GetMetadata, 
    //    };

    //    await ExecuteAsync(parameters, cancellationToken).ConfigureAwait(false);
    //    return mediaFile.Metadata;
    //}

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

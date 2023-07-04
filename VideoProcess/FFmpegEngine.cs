using VideoProcess.NET.Domain;

namespace VideoProcess.NET;

public class FFmpegEngine
{
    private readonly string _ffmpegPath;

    public FFmpegEngine(string? ffmpegPath = null)
    {
        ffmpegPath ??= Constants.FfmpegExecutable;
        _ffmpegPath = ffmpegPath.TryGetFfmpegPath(); 
    } 
}

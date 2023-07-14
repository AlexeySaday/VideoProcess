using VideoProcess.NET.Input;
using VideoProcess.NET.Output;

namespace VideoProcess.NET;

public class FfmpegParameter
{
    public ConvertOptions? ConvertOptions { get; set; }
    public required FFmpegTaskType TaskType { get; set; }
    public required IInputArgument InputArgument { get; set; }
    public required IOutputArgument OutputArgument { get; set; }
}
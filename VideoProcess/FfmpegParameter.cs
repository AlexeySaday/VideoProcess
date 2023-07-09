using VideoProcess.NET.Input;
using VideoProcess.NET.Output;

namespace VideoProcess.NET;

public class FfmpegParameter
{
    public required FFmpegTaskType TaskType { get; set; }
    public required IInputArgument InputArgument { get; set; }
    public IOutputArgument? OutputArgument { get; set; }
}
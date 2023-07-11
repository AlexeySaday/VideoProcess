using VideoProcess.NET.Input;

namespace VideoProcess.NET;

public static class FfmpegStringBuilder
{
    public static string BuildString(FfmpegParameter parameter)
    {
        return parameter.TaskType switch
        {
            //FFmpegTaskType.GetMetadata => GetMetadata(parameter.InputArgument),
            _ => throw new NotImplementedException(),
        };
    }

    private static string GetMetadata(IInputArgument input)
    {
        return $"-i {input.Argument} -nostdin -f ffmetadata -"; 
    }
}
using VideoProcess.NET.Modify;

namespace VideoProcess.NET;

public class ConvertOptions
{
    public string? CustomConversionFfmpegArgument { get; set; } = null;

    public Crop? Crop { get; set; } 
    public Trim? Trim { get; set; } 
}
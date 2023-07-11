using VideoProcess.NET.Modify;

namespace VideoProcess.NET;

public class ConvertOptions
{
    public string? CustomConversionFfmpegArgument { get; set; }

    public Crop? Crop { get; set; } 

    public int? Bitrates { get; set; } 
}
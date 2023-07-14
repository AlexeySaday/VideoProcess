using System.Text;
using VideoProcess.NET.Input;

namespace VideoProcess.NET;

public static class FfmpegStringBuilder
{
    public static string BuildString(FfmpegParameter parameter)
    {
        return parameter.TaskType switch
        {
            FFmpegTaskType.Convert => 
        $" -i {parameter.InputArgument.Argument} {Convert(parameter.ConvertOptions!)} {parameter.OutputArgument.Argument}",
            //FFmpegTaskType.GetMetadata => GetMetadata(parameter.InputArgument),
            _ => throw new NotImplementedException(),
        };
    }

    private static string GetMetadata(IInputArgument input)
    {
        return $"-i {input.Argument} -nostdin -f ffmetadata -";
    }

    // @TODO FIXME Сделать отдельной опцией '-c:v copy -c:a copy' 
    private static string Convert(ConvertOptions options)
    {
        if (options == null)
        {
            return string.Empty;
        }
        var argumentBuilder = new StringBuilder();

        if (options.Trim != null)
        {
            var start = options.Trim.StartSec != null ? $" -ss {ConvertToTimeFormat(options.Trim.StartSec)} " : string.Empty;
            var end = options.Trim.EndSec != null ? $" -t {ConvertToTimeFormat(options.Trim.EndSec)}" : string.Empty;
            argumentBuilder.Append($" {start} {end} ");
        }
        if (options.Crop != null)
        {
            argumentBuilder.Append(
                $" -vf \"crop={options.Crop.With}:{options.Crop.Height}:{options.Crop.CropX}:{options.Crop.CropY}\" "
            );
        }

        return argumentBuilder.ToString();
    }

    public static string ConvertToTimeFormat(int? totalSeconds)
    {
        var hours = totalSeconds / 3600;
        var minutes = (totalSeconds % 3600) / 60;
        var seconds = totalSeconds % 60;

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}


using VideoProcess.NET.Input;
using VideoProcess.NET.Output;

namespace VideoProcess.NET.Tests;

public class InputFileTest
{
    [Fact]
    public async void Simple()
    {
        var cancellationTokenRegistration = new CancellationTokenRegistration();
         
        var engine = new FfmpegEngine();
        var input = new InputFile(@"C:\Users\Hp\Downloads\wall_4.mp4");
        var output = new OutputFile(@"C:\Users\Hp\Downloads\wall_4_output.mp4");
        var convert = new ConvertOptions
        {
            Crop = new(1000, 500), 
        };
        await engine.ConvertAsync(input, output, convert, cancellationTokenRegistration.Token);
    }
}
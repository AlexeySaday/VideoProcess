using VideoProcess.NET;
using VideoProcess.NET.Input;
using VideoProcess.NET.Output;

var cancellationTokenRegistration = new CancellationTokenRegistration();

var engine = new FfmpegEngine();
var input = new InputFile(@"C:\Users\Hp\Downloads\wall_4.mp4");
var output = new OutputFile(@"C:\Users\Hp\Downloads\wall_4_output.mp4");
var convert = new ConvertOptions
{
    Trim = new(2, 3),
    Crop = new(500, 500),
};
await engine.ConvertAsync(input, output, convert, cancellationTokenRegistration.Token);
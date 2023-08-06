using VideoProcess.NET.Input;
using VideoProcess.NET.Output;

namespace VideoProcess.NET.Tests;

public class InputStreamTests
{
    private const string videoFilePath = @"C:\Users\Hp\Downloads\wall_4.mp4";
    private readonly Stream _stream;

    public InputStreamTests()
    {
        _stream = new FileStream(videoFilePath, FileMode.Open, FileAccess.Read);
    }

    [Fact]
    public async void BaseStreamTest()
    {
        var ctr = new CancellationTokenRegistration();

        var input = new InputStream(_stream);
        var output = new OutputFile(@"C:\Users\Hp\Downloads\wall_4_output.mp4");

        var engine = new FfmpegEngine();

        var convert = new ConvertOptions
        {
            Trim = new Modify.Trim(1, 5),
        };
        await engine.ConvertAsync(input, output, convert, ctr.Token);
    }
}
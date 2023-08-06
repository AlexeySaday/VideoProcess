using VideoProcess.NET.Input;
using VideoProcess.NET.Output;

namespace VideoProcess.NET.Tests;

public class InputByteArrayTests
{
    private const string videoFilePath = @"C:\Users\Hp\Downloads\wall_4.mp4";

    private readonly ReadOnlyMemory<byte> _bytes;

    public InputByteArrayTests()
    {
        _bytes = new ReadOnlyMemory<byte>(File.ReadAllBytes(videoFilePath));
    }

    [Fact]
    public async void BaseStreamTest()
    {
        var ctr = new CancellationTokenRegistration();

        var input = new InputByteArray(_bytes);
        var output = new OutputFile(@"C:\Users\Hp\Downloads\wall_4_output.mp4");

        var engine = new FfmpegEngine();

        var convert = new ConvertOptions
        {
            Trim = new Modify.Trim(1, 4),
        };
        await engine.ConvertAsync(input, output, convert, ctr.Token);
    }
}
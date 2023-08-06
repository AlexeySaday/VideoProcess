using System.Diagnostics;

namespace VideoProcess.NET.Input;

public class InputByteArray : IInputArgument, IProcessHandler
{
    public string Argument => "-";

    private readonly ReadOnlyMemory<byte> _data;

    public InputByteArray(byte[] data, int offset, int count)
    {
        _data = new ReadOnlyMemory<byte>(data, offset, count);
    }

    public InputByteArray(ReadOnlyMemory<byte> data)
    {
        _data = data;
    }

    public async Task HandleProcessStartedAsync(Process process, CancellationToken cancellationToken)
    {
        if (process is null || process.HasExited)
        {
            return;
        }

        try
        {
            var inputStream = process.StandardInput.BaseStream;
            await inputStream.WriteAsync(_data, cancellationToken);
        }
        catch (IOException ioException) when (ioException.HResult == Constants.ChannelClosedHResult)
        {
            // If input stream has already closed, ignore it.
        }
        catch (InvalidOperationException)
        {
            // If the process doesn't exist anymore, ignore it.
        }
    }
}
using System.Diagnostics;

namespace VideoProcess.NET.Input;

public class InputStream : IInputArgument, IProcessHandler, IDisposable, IAsyncDisposable
{
    private const int ChannelClosedHResult = -2147024787;
    private const int ReadBufferSize = 64 * 1024; // 64Kb

    private readonly Stream _stream; 
    public string Argument => "-";

    public InputStream(Stream stream)
    {
        if (stream is null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        if (!stream.CanRead)
        {
            throw new ArgumentException("Input stream should be readable!");
        }

        _stream = stream;
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
            var buffer = new byte[ReadBufferSize];
            var bytesRead = 0;

            do
            {
                bytesRead = await _stream.ReadAsync(buffer, 0, ReadBufferSize, cancellationToken)
                    .ConfigureAwait(false);

                await inputStream.WriteAsync(buffer, 0, bytesRead, cancellationToken)
                    .ConfigureAwait(false);

            } while (bytesRead > 0 && !process.HasExited);
        }
        catch (IOException ioException) when (ioException.HResult == ChannelClosedHResult)
        {
            // If input stream has already closed, ignore it.
        }
        catch (InvalidOperationException)
        {
            // If the process doesn't exist anymore, ignore it.
        }

        Close(process);
    }

    public void Dispose()
    {
        _stream.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _stream.DisposeAsync();
    }

    private static void Close(Process process)
    {
        if (process is null || process.HasExited)
        {
            return;
        }
        try
        {
            process.StandardInput.Close();
        }
        catch (InvalidOperationException)
        {
            // Ignore if doesn't exist
        }
    }
}
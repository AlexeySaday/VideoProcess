using System.Diagnostics;
using VideoProcess.NET.Extensions;

namespace VideoProcess.NET;

public class FfmpegProcess
{
    private readonly FfmpegParameter _parameters;
    private readonly string _ffmpegPath;

    //private MediaInfo _mediaInfo;
    //private List<string> _messages;
    //private Exception _caughtException = null;

    public FfmpegProcess(FfmpegParameter parameters, string ffmpegFilePath)
    {
        _parameters = parameters;
        _ffmpegPath = ffmpegFilePath;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    { 
        var arguments = FfmpegStringBuilder.BuildString(_parameters);
        ProcessStartInfo startInfo = GenerateStartInfo(_ffmpegPath, arguments);
        await ExecuteAsync(startInfo, _parameters, cancellationToken).ConfigureAwait(false);
    }

    private async Task ExecuteAsync(ProcessStartInfo startInfo, FfmpegParameter parameters, CancellationToken cancellationToken)
    {
        using var ffmpegProcess = new Process() { StartInfo = startInfo };
        //ffmpegProcess.ErrorDataReceived += OnDataHandler;

        Task<int>? task = null;
        try
        {
            //var useStandardInput = _parameters.InputArgument.UseStandardInput == true;
            task = ffmpegProcess.WaitForExit(cancellationToken);

            //var inputHandler = _parameters.Input as IProcessExecutionHandler;

            //if (inputHandler != null)
            //{
            //    await inputHandler.HandleProcessStartedAsync(ffmpegProcess, cancellationToken).ConfigureAwait(false);
            //}

            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception)
            {
                // An exception occurs if the user cancels the operation by calling Cancel on the CancellationToken.
                // Exc.Message will be "A task was canceled." (in English).
                // task.IsCanceled will be true.
                if (task.IsCanceled)
                {
                    throw new TaskCanceledException(task);
                }
                // I don't think this can occur, but if some other exception, rethrow it.
                throw;
            }

            //if (inputHandler != null)
            //{
            //    await inputHandler.HandleProcessExitedAsync(ffmpegProcess, cancellationToken).ConfigureAwait(false);
            //}
        }
        finally
        {
            task?.Dispose();
            //ffmpegProcess.ErrorDataReceived -= OnDataHandler;
        }


        //if (_caughtException != null || ffmpegProcess.ExitCode != 0)
        //{
        //    OnException(_messages, parameters, ffmpegProcess.ExitCode, _caughtException);
        //}
        //else
        //{
        //    OnConversionCompleted(new ConversionCompleteEventArgs(parameters.Input, parameters.Output));
        //}
    }

    private static ProcessStartInfo GenerateStartInfo(string ffmpegPath, string arguments) => new()
    {
        // -y overwrite output files
        Arguments = "-y " + arguments,
        FileName = ffmpegPath,
        CreateNoWindow = true,
        RedirectStandardInput = true,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        WindowStyle = ProcessWindowStyle.Hidden
    };
}
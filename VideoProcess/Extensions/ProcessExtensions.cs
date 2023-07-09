using System.Diagnostics;

namespace VideoProcess.NET.Extensions;

public static class ProcessExtensions
{
    public static Task<int> WaitForExit(this Process process, CancellationToken cancellationToken)
    {
        var ctRegistration = new CancellationTokenRegistration();
        var mustUnregister = false;
        TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
        if (cancellationToken != default)
        {
            mustUnregister = true;
            ctRegistration = cancellationToken.Register(() =>
            {
                try
                {  
                    // If standard input used for data input just close it.
                    // It will force process to stop (closing files).
                    process.StandardInput.Close(); 
                }
                catch (InvalidOperationException)
                {
                    // If the process doesn't exist anymore, ignore it.
                }
                finally
                {
                    // Cancel the task. This will throw an exception to the calling program.
                    // Exc.Message will be "A task was canceled."
                    try
                    {
                        tcs.SetCanceled();
                    }
                    catch (Exception)
                    {
                    }
                }
            });
        }

        void processOnExited(object sender, EventArgs e)
        {
            process.WaitForExit();
            //if (process.ExitCode != 0) 
                //onException?.Invoke(process.ExitCode);
            tcs.TrySetResult(process.ExitCode);
            if (mustUnregister) ctRegistration.Dispose();
            process.Exited -= processOnExited;
        }

        process.EnableRaisingEvents = true;
        process.Exited += processOnExited;

        var started = process.Start();
        if (!started)
            tcs.TrySetException(new InvalidOperationException($"Could not start process {process}"));

        process.BeginErrorReadLine();

        return tcs.Task;
    }
}
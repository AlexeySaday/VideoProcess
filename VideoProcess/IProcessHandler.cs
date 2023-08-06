using System.Diagnostics;

namespace VideoProcess.NET;

public interface IProcessHandler
{
    Task HandleProcessStartedAsync(Process process, CancellationToken cancellationToken = default);
}
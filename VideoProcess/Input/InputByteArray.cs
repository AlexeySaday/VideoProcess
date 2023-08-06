using System.Diagnostics;

namespace VideoProcess.NET.Input;

public class InputByteArray : IInputArgument, IProcessHandler
{


    public string Argument => throw new NotImplementedException();

    public Task HandleProcessStartedAsync(Process process, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
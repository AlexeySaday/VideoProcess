namespace VideoProcess.NET.Exceptions.FileNotFound;

public class FfmpegNotFoundException : BaseFileNotFoundException
{  
    public FfmpegNotFoundException(string path) : base(path) { }

    public override string Message => WrongFilePath != Constants.FfmpegExecutable ?
        $"{Constants.FfmpegExecutable} not found by this path {WrongFilePath}" : $"{Constants.FfmpegExecutable} not found";
}
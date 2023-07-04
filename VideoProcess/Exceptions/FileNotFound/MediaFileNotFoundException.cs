namespace VideoProcess.NET.Exceptions.FileNotFound;

public class MediaFileNotFoundException : BaseFileNotFoundException
{ 
    public MediaFileNotFoundException(string wrongPath) : base(wrongPath) { }

    public override string Message => $"This {WrongFilePath} file path not found";
}
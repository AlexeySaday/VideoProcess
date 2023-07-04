namespace VideoProcess.NET.Exceptions.FileNotFound;

public class BaseFileNotFoundException : Exception
{
    private readonly string _wrongFilePath;

    public string WrongFilePath { get => _wrongFilePath; }

    public BaseFileNotFoundException(string wrongFilePath) 
    {
        _wrongFilePath = wrongFilePath;
    }
}
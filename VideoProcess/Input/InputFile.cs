using VideoProcess.NET.Exceptions.FileNotFound;

namespace VideoProcess.NET.Input;

public class InputFile : IInputArgument
{
    private readonly FileInfo _fileInfo;

    public InputFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new MediaFileNotFoundException(filePath);
        }
        _fileInfo = new FileInfo(filePath); 
    }

    public string Argument => $"\"{_fileInfo.FullName}\"";
}
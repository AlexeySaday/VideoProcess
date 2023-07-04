using VideoProcess.NET.Exceptions.FileNotFound;

namespace VideoProcess.NET.Output;

public class OutputFile : IArgument
{
    private readonly FileInfo _fileInfo;

    public OutputFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new MediaFileNotFoundException(filePath);
        }
        _fileInfo = new FileInfo(filePath);
    }

    public string Argument => $"\"{_fileInfo.FullName}\"";
}
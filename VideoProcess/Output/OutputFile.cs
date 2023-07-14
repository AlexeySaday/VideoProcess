using System.Diagnostics;
using VideoProcess.NET.Exceptions.FileNotFound;

namespace VideoProcess.NET.Output;

public class OutputFile : IOutputArgument 
{
    private readonly FileInfo _fileInfo;

    public OutputFile(string filePath)
    {
        // @TODO FIXME Сделать настройки: перезаписывать или не перезаписывать файл, если он существует
        if (!File.Exists(filePath))
        {
            Debug.WriteLine("File will be overwritten");
        }
        _fileInfo = new FileInfo(filePath);
    }

    public string Argument => $"\"{_fileInfo.FullName}\"";
     
}
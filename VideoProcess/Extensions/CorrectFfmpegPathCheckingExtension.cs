using VideoProcess.NET.Exceptions.FileNotFound;

namespace VideoProcess.NET.Domain;

public static class CorrectFfmpegPathCheckingExtension
{
    public static string TryGetFfmpegPath(this string path)
    {
        string fullPath;
        if (path.TryGetFullPathIfFileExists(out fullPath)) return fullPath;
        if (path.TryGetFullPathIfPathEnvironmentExists(out fullPath)) return fullPath;

        throw new FfmpegNotFoundException(path);
    }

    private static bool TryGetFullPathIfFileExists(this string filePath, out string fullPath)
    {
        fullPath = string.Empty;

        if (!File.Exists(filePath)) return false;

        fullPath = Path.GetFullPath(filePath);
        return true;
    }

    private static bool TryGetFullPathIfPathEnvironmentExists(this string fileName, out string fullPath)
    {
        fullPath = string.Empty;
        var values = Environment.GetEnvironmentVariable("PATH");
        var pathElements = values?.Split(Path.PathSeparator);

        if (pathElements == null) return false;

        foreach (var path in pathElements)
        {
            var tempFullPath = Path.Combine(path, fileName);
            if (tempFullPath.TryGetFullPathIfFileExists(out fullPath))
            {
                return true;
            }
        }

        return false;
    }
}
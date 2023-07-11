namespace VideoProcess.NET.Modify;

public class Crop
{
    internal int CropX { get; } = 0;
    internal int CropY { get; } = 0;
    internal int With { get; }
    internal int Height { get; }

    public Crop(int width, int height)
    {
        With = width;
        Height = height;
    }

    public Crop(int cropX, int cropY, int with, int height)
    {
        With = with;
        Height = height;
        CropX = cropX;
        CropY = cropY;
    }
}
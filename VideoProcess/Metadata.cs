namespace VideoProcess.NET;

public class Metadata
{ 
    public TimeSpan Duration { get; internal set; }
    public Video? VideoData { get; internal set; }
    public Audio? AudioData { get; internal set; } 

    public override string ToString()
        => $"Duration: {Duration}\nVideo MetaData:\n{VideoData}\nAudio MetaData:\n{AudioData}";

    public class Video
    { 
        public string Format { get; internal set; } = string.Empty;
        public string ColorModel { get; internal set; } = string.Empty;
        public string FrameSize { get; internal set; } = string.Empty;
        public int? BitRateKbs { get; internal set; }
        public double Fps { get; internal set; }

        public override string ToString()
            => $"Format: {Format}\nColorModel: {ColorModel}\nFrameSize: {FrameSize}\nBitRateKbs: {BitRateKbs}\nFps: {Fps}";
    }

    public class Audio
    {
        public string Format { get; internal set; } = string.Empty;
        public string SampleRate { get; internal set; } = string.Empty;
        public string ChannelOutput { get; internal set; } = string.Empty;
        public int BitRateKbs { get; internal set; }

        public override string ToString()
            => $"Format: {Format}\nSampleRate: {SampleRate}\nChannelOuput: {ChannelOutput}\nBitRateKbs: {BitRateKbs}";
    }

    public interface IHasMetaData
    {
        Metadata Metadata { get; set; }
    }
}
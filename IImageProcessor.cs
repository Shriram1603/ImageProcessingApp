using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageProcessingApp.Plugins;

/// <summary>
/// Interface to be implemented by classes
/// to work as pligin
/// </summary>
public interface IImageProcessor
{
    string Name { get; }  // Plugin name
    Image<Rgba32> ProcessImage(Image<Rgba32> image);
}


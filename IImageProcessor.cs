using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageProcessingApp.Plugins;

/// <summary>
/// Interface to be implemented by classes
/// to work as pligin
/// </summary>
public interface IImageProcessor
{
    /// <summary>
    /// Name of the plugin
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Process that the function performs 
    /// on the image
    /// </summary>
    /// <param name="image"><see cref="Image<Rgba32>"/> type</param>
    /// <returns>><see cref="Image<Rgba32>"/></returns>
    Image<Rgba32> ProcessImage(Image<Rgba32> image);
}


using System;
using System.IO;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using ImageProcessingApp.Plugins;

namespace ImageProcessingApp;

class Program
{
    static void Main(string[] args)
    {
        string pluginFolder = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
        var plugins = PluginLoader.LoadPlugins(pluginFolder);

        if (plugins.Count == 0)
        {
            Console.WriteLine("No plugins found!");
            return;
        }

        Console.WriteLine("Available Plugins:");
        for (int i = 0; i < plugins.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {plugins[i].Name}");
        }

        Console.Write("Select a plugin : ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= plugins.Count)
        {
            Console.Write("Enter the path of the image: ");
            string imagePath = Console.ReadLine();

            if (File.Exists(imagePath))
            {
                using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
                {
                    IImageProcessor selectedPlugin = plugins[choice - 1];
                    using (Image<Rgba32> processedImage = selectedPlugin.ProcessImage(image))
                    {
                        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "output.png");
                        processedImage.Save(outputPath);
                        Console.WriteLine($"Processed image saved at: {outputPath}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid image path.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
}


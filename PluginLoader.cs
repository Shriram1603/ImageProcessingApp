using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using ImageProcessingApp.Plugins;

/// <summary>
/// Holds the responsibility of functions that
/// send back plugins to <see cref="Program.Main"/>
/// </summary>
public static class PluginLoader
{
    /// <summary>
    /// Loads in all the dll available in the mentioned folder
    /// and returns all the plugins from assembly
    /// </summary>
    /// <param name="pluginFolder">Folder name of where the dll's are</param>
    /// <returns>list of all classes that implements <see cref="IImageProcessor"/> from dll </returns>
    public static List<IImageProcessor> LoadPlugins(string pluginFolder)
    {
        List<IImageProcessor> plugins = new List<IImageProcessor>();

        if (!Directory.Exists(pluginFolder))
        {
            Console.WriteLine($"[Error] Plugin directory not found: {pluginFolder}");
            return plugins;
        }

        string[] files = Directory.GetFiles(pluginFolder, "*.dll");
        Console.WriteLine($"[Info] DLLs in Plugin Folder: {string.Join(", ", files)}");

        foreach (string file in files)
        {
            try
            {
                Console.WriteLine($"[Info] Loading DLL: {file}");
                Assembly assembly = Assembly.LoadFile(file);
                
                var types = assembly.GetTypes().Where(t => typeof(IImageProcessor).IsAssignableFrom(t) && !t.IsInterface);
                foreach (var type in types)
                {
                    if (Activator.CreateInstance(type) is IImageProcessor plugin)
                    {
                        Console.WriteLine($"[Success] Loaded Plugin: {plugin.Name}");
                        plugins.Add(plugin);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to load plugin from {file}: {ex.Message}");
            }
        }

        return plugins;
    }
}

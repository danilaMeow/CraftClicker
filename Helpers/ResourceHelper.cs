using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MyApp.Models;

namespace MyApp.Helpers;

public static class ResourceHelper
{
    private static List<ResourceConfig> _resources = new();

    static ResourceHelper()
    {
        LoadData();
    }

    private static void LoadData()
    {
        string filePath = "ResourcesData.json";
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                _resources = JsonSerializer.Deserialize<List<ResourceConfig>>(json) ?? new();
            }
            catch
            {
                _resources = new();
            }
        }
    }

    public static ResourceConfig? GetConfig(string name)
    {
        return _resources.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public static (string category, string icon) GetResourceDetails(string name)
    {
        var config = GetConfig(name);
        return config != null ? (config.Category, config.Icon) : ("Разное", "📦");
    }
}
namespace MyApp.Models;

public class ResourceConfig
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Icon { get; set; } = "📦";
    public int DefaultDurability { get; set; } = 4;
    public bool IsRare { get; set; } = false;
}
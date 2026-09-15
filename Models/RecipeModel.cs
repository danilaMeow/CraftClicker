using System.Collections.Generic;

namespace MyApp.Models;

public class IngredientModel
{
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class RecipeModel
{
    public string ResultName { get; set; } = string.Empty;
    public string ResultCategory { get; set; } = "Разное";
    public string ResultIcon { get; set; } = "📦";
    public int ResultCount { get; set; } = 1;
    public List<IngredientModel> Ingredients { get; set; } = new();
}
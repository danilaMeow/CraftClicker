using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.ViewModels;

public partial class CraftViewModel : ObservableObject
{
    private readonly InventoryService _inventory;

    [ObservableProperty]
    private ObservableCollection<RecipeModel> _recipes = new();

    [ObservableProperty]
    private RecipeModel? _selectedRecipe;

    [ObservableProperty]
    private string _statusText = "Выберите рецепт";

    public CraftViewModel(InventoryService inventory)
    {
        _inventory = inventory;
        LoadRecipes();
    }

    private void LoadRecipes()
    {
        string path = "RecipesData.json";
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                var items = JsonSerializer.Deserialize<ObservableCollection<RecipeModel>>(json);
                if (items != null) Recipes = items;
            }
            catch { }
        }
    }

    [RelayCommand]
    private void Craft()
    {
        if (SelectedRecipe == null) return;

        // Проверяем наличие ингредиентов
        foreach (var ing in SelectedRecipe.Ingredients)
        {
            var invItem = _inventory.Items.FirstOrDefault(i => i.Name == ing.Name);
            if (invItem == null || invItem.Count < ing.Count)
            {
                StatusText = $"Не хватает: {ing.Name} ({ing.Count} шт)";
                return;
            }
        }

        // Забираем ингредиенты
        foreach (var ing in SelectedRecipe.Ingredients)
        {
            var invItem = _inventory.Items.First(i => i.Name == ing.Name);
            invItem.Count -= ing.Count;
        }

        // Добавляем скрафченный предмет
        _inventory.AddItem(
            SelectedRecipe.ResultName,
            SelectedRecipe.ResultCount,
            SelectedRecipe.ResultCategory,
            SelectedRecipe.ResultIcon
        );

        StatusText = $"Создано: {SelectedRecipe.ResultName}!";
    }
}
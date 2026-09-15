using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Models;

namespace MyApp.Services;

public partial class InventoryService : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<InventoryItemModel> _items = new();

    [ObservableProperty]
    private ObservableCollection<InventoryItemModel> _filteredItems = new();

    [ObservableProperty]
    private string _selectedCategory = "Шахта";

    public InventoryService()
    {
        UpdateFilter();
    }

    [RelayCommand]
    private void SelectCategory(string category)
    {
        SelectedCategory = category;
        UpdateFilter();
    }

    public void AddItem(string name, int amount, string category, string icon = "📦")
    {
        var existing = Items.FirstOrDefault(i => i.Name == name);
        if (existing != null)
        {
            existing.Count += amount;
        }
        else
        {
            Items.Add(new InventoryItemModel(name, amount, category, icon));
        }

        UpdateFilter();
    }

    public void UpdateFilter()
    {
        // Удаляем из общего списка предметы, которых осталось 0
        var emptyItems = Items.Where(i => i.Count <= 0).ToList();
        foreach (var item in emptyItems)
        {
            Items.Remove(item);
        }

        FilteredItems.Clear();
        var matchingItems = Items.Where(i => i.Category == SelectedCategory);
        foreach (var item in matchingItems)
        {
            FilteredItems.Add(item);
        }
    }


}
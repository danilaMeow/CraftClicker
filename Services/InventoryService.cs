using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using MyApp.Models;

namespace MyApp.Services;

public partial class InventoryService : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<InventoryItemModel> _items = new();

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
    }
}
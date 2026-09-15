using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;

namespace MyApp.Models;

public partial class InventoryItemModel : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private int _count;

    [ObservableProperty]
    private string _category = "Шахта"; // "Шахта", "Лес", "Поле", "Монстры"

    [ObservableProperty]
    private string _icon = "📦";

    public InventoryItemModel(string name, int count, string category, string icon = "📦")
    {
        Name = name;
        Count = count;
        Category = category;
        Icon = icon;
    }
}
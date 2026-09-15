using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Services;

namespace MyApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private object _currentView;

    public InventoryService Inventory { get; } = new();

    public MineViewModel MineVM { get; }
    public HarvestLocationViewModel ForestVM { get; }
    public HarvestLocationViewModel FieldVM { get; }
    public MonstersViewModel MonstersVM { get; }
    public CraftViewModel CraftVM { get; }

    public MainViewModel()
    {
        MineVM = new MineViewModel(Inventory);
        ForestVM = new HarvestLocationViewModel("🪓 ЛЕС", "Дерево", "Вековое Дерево", Inventory);
        FieldVM = new HarvestLocationViewModel("🌾 ПОЛЕ", "Пшеница", "Золотая Пшеница", Inventory);
        MonstersVM = new MonstersViewModel(Inventory);
        CraftVM = new CraftViewModel(Inventory);

        CurrentView = MineVM;
    }

    [RelayCommand]
    private void SelectLocation(string location)
    {
        CurrentView = location switch
        {
            "Mine" => MineVM,
            "Forest" => ForestVM,
            "Field" => FieldVM,
            "Monsters" => MonstersVM,
            "Craft" => CraftVM,
            _ => CurrentView
        };
    }

    public void HandleRKey()
    {
        if (CurrentView is MineViewModel mine && mine.ResetGridCommand.CanExecute(null))
            mine.ResetGridCommand.Execute(null);
        else if (CurrentView is HarvestLocationViewModel harvest && harvest.ResetGridCommand.CanExecute(null))
            harvest.ResetGridCommand.Execute(null);
        else if (CurrentView is MonstersViewModel monsters && monsters.ResetGridCommand.CanExecute(null))
            monsters.ResetGridCommand.Execute(null);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private object _currentView;

    public MineViewModel MineVM { get; } = new();
    public HarvestLocationViewModel ForestVM { get; } = new("🪓 ЛЕС", "Дерево", "Вековое Дерево");
    public HarvestLocationViewModel FieldVM { get; } = new("🌾 ПОЛЕ", "Пшеница", "Золотая Пшеница");
    public MonstersViewModel MonstersVM { get; } = new();

    public MainViewModel()
    {
        CurrentView = MineVM; // По умолчанию открываем Шахту
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
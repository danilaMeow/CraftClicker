using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Helpers;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.ViewModels;

public partial class HarvestLocationViewModel : ObservableObject
{
    private readonly InventoryService _inventory;

    [ObservableProperty]
    private ObservableCollection<ResourceCellModel> _cells = new();

    [ObservableProperty]
    private int _minedCount = 0;

    [ObservableProperty]
    private bool _canReset = false;

    [ObservableProperty]
    private string _statusText = "Добудьте минимум 4 ресурса для сброса";

    [ObservableProperty]
    private string _title = "Локация";

    private readonly string _defaultResource;
    private readonly string _rareResource;
    private readonly Random _random = new();

    public HarvestLocationViewModel(string title, string defaultResource, string rareResource, InventoryService inventory)
    {
        Title = title;
        _defaultResource = defaultResource;
        _rareResource = rareResource;
        _inventory = inventory;
        GenerateGrid();
    }

    [RelayCommand]
    private void ClickCell(ResourceCellModel cell)
    {
        if (cell == null || cell.IsMined) return;

        cell.Hit(1);

        if (cell.IsMined)
        {
            MinedCount++;
            var (category, icon) = ResourceHelper.GetResourceDetails(cell.Name);
            _inventory.AddItem(cell.Name, 1, category, icon);

            CheckResetCondition();
            if (_minedCount >= 9) ResetGrid();
        }
    }

    [RelayCommand]
    private void ResetGrid()
    {
        if (!_canReset && _minedCount < 9) return;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _cells.Clear();
        MinedCount = 0;
        CanReset = false;
        StatusText = "Добудьте минимум 4 ресурса для сброса";

        for (int i = 0; i < 9; i++)
        {
            bool isRareRoll = _random.Next(100) < 15;
            string resourceName = isRareRoll ? _rareResource : _defaultResource;

            var config = ResourceHelper.GetConfig(resourceName);
            int hp = config?.DefaultDurability ?? 4;
            bool isRare = config?.IsRare ?? false;

            _cells.Add(new ResourceCellModel
            {
                Name = resourceName,
                MaxDurability = hp,
                CurrentDurability = hp,
                IsGoldenNode = isRare,
                IsMined = false
            });
        }
    }

    private void CheckResetCondition()
    {
        if (_minedCount >= 4)
        {
            CanReset = true;
            StatusText = "Готово к пересозданию! (Кнопка или R)";
        }
        else
        {
            StatusText = $"Добыто: {_minedCount}/4 для сброса";
        }
    }
}
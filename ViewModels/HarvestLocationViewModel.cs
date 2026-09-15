using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Models;

namespace MyApp.ViewModels;

public partial class HarvestLocationViewModel : ObservableObject
{
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

    public HarvestLocationViewModel(string title, string defaultResource, string rareResource)
    {
        Title = title;
        _defaultResource = defaultResource;
        _rareResource = rareResource;
        GenerateGrid();
    }

    [RelayCommand]
    private void ClickCell(ResourceCellModel cell)
    {
        if (cell == null || cell.IsMined) return;

        int toolDamage = 1; // В будущем профильный урон от Топора/Лопаты
        cell.Hit(toolDamage);

        if (cell.IsMined)
        {
            MinedCount++;
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
            bool isRare = _random.Next(100) < 15;
            _cells.Add(new ResourceCellModel
            {
                Name = isRare ? _rareResource : _defaultResource,
                MaxDurability = isRare ? 8 : 4,
                CurrentDurability = isRare ? 8 : 4,
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

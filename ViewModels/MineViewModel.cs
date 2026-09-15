using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Models;

namespace MyApp.ViewModels;

public partial class MineViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ResourceCellModel> _cells = new();

    [ObservableProperty]
    private int _minedCount = 0;

    [ObservableProperty]
    private bool _canReset = false;

    [ObservableProperty]
    private string _statusText = "Добудьте минимум 4 ресурса для сброса";

    private readonly Random _random = new();

    public MineViewModel()
    {
        GenerateGrid();
    }

    [RelayCommand]
    private void ClickCell(ResourceCellModel cell)
    {
        if (cell == null || cell.IsMined) return;

        int pickaxeDamage = 1;
        cell.Hit(pickaxeDamage);

        if (cell.IsMined)
        {
            MinedCount++;
            CheckResetCondition();

            if (_minedCount >= 9)
            {
                ResetGrid();
            }
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
            bool isGold = _random.Next(100) < 15;
            _cells.Add(new ResourceCellModel
            {
                Name = isGold ? "Золотая жила" : "Каменная жила",
                MaxDurability = isGold ? 10 : 5,
                CurrentDurability = isGold ? 10 : 5,
                IsGoldenNode = isGold,
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
using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Models;

namespace MyApp.ViewModels;

public partial class MonstersViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<MonsterCellModel> _monsters = new();

    [ObservableProperty]
    private int _killedCount = 0;

    [ObservableProperty]
    private bool _canReset = false;

    [ObservableProperty]
    private string _statusText = "Убейте минимум 2 монстров для сброса";

    private readonly Random _random = new();
    private readonly string[] _monsterNames = { "Слайм", "Гоблин", "Скелет", "Орк" };

    public MonstersViewModel()
    {
        GenerateGrid();
    }

    [RelayCommand]
    private void ClickMonster(MonsterCellModel monster)
    {
        if (monster == null || monster.IsDead) return;

        int swordDamage = 2; // Урон меча
        monster.Hit(swordDamage);

        if (monster.IsDead)
        {
            KilledCount++;
            CheckResetCondition();

            if (_killedCount >= 5) ResetGrid();
        }
    }

    [RelayCommand]
    private void ResetGrid()
    {
        if (!_canReset && _killedCount < 5) return;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _monsters.Clear();
        KilledCount = 0;
        CanReset = false;
        StatusText = "Убейте минимум 2 монстров для сброса";

        for (int i = 0; i < 5; i++)
        {
            string name = _monsterNames[_random.Next(_monsterNames.Length)];
            int hp = _random.Next(8, 16);
            _monsters.Add(new MonsterCellModel
            {
                Name = name,
                MaxHp = hp,
                CurrentHp = hp,
                IsDead = false
            });
        }
    }

    private void CheckResetCondition()
    {
        if (_killedCount >= 2)
        {
            CanReset = true;
            StatusText = "Готово к пересозданию! (Кнопка или R)";
        }
        else
        {
            StatusText = $"Убито: {_killedCount}/2 для сброса";
        }
    }
}
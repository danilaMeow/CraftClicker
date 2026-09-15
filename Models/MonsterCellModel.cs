using CommunityToolkit.Mvvm.ComponentModel;

namespace MyApp.Models;

public partial class MonsterCellModel : ObservableObject
{
    [ObservableProperty]
    private string _name = "Гоблин";

    [ObservableProperty]
    private int _maxHp = 10;

    [ObservableProperty]
    private int _currentHp = 10;

    [ObservableProperty]
    private bool _isDead = false;

    public void Hit(int damage)
    {
        if (_isDead) return;

        CurrentHp -= damage;
        if (_currentHp <= 0)
        {
            CurrentHp = 0;
            IsDead = true;
        }
    }
}
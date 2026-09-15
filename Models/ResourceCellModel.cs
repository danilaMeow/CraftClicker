using CommunityToolkit.Mvvm.ComponentModel;

namespace MyApp.Models;

public partial class ResourceCellModel : ObservableObject
{
    [ObservableProperty]
    private string _name = "Камень";

    [ObservableProperty]
    private int _maxDurability = 5;

    [ObservableProperty]
    private int _currentDurability = 5;

    [ObservableProperty]
    private bool _isMined = false;

    [ObservableProperty]
    private bool _isGoldenNode = false;

    public void Hit(int damage)
    {
        if (_isMined) return;

        CurrentDurability -= damage;
        if (_currentDurability <= 0)
        {
            CurrentDurability = 0;
            IsMined = true;
        }
    }
}
using System.Windows;
using System.Windows.Input;
using MyApp.ViewModels;

namespace MyApp;

public partial class MainWindow : Window
{
    public MainViewModel MainVM { get; } = new MainViewModel();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = MainVM;
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.R)
        {
            MainVM.HandleRKey();
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
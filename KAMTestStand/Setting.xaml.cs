using System.Windows;

namespace KAMTestStand;

public partial class Setting : Window
{
    public Setting(Window parent)
    {
        InitializeComponent();
        _parent = parent;
    }

    private readonly Window _parent;
    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        _parent.Show();
        Close();
    }

    private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
    {
        _parent.Show();
        Close();
    }
}
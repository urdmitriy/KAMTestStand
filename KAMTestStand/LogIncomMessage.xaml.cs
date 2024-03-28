using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using String = System.String;

namespace KAMTestStand;

public partial class LogIncomMessage : Window
{
    public LogIncomMessage(List<String> logIncomMessageList)
    {
        InitializeComponent();
        _logIncomMessageList = logIncomMessageList;
        ListBoxData.ItemsSource = _logIncomMessageList;
    }

    private readonly List<String> _logIncomMessageList;
    
    private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
    {
        _logIncomMessageList.Clear();
        Dispatcher.Invoke(() => ListBoxData.Items.Refresh());
    }

    private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
    {
        Hide();
    }
    public void UpdateData()
    {
        Dispatcher.Invoke(() => ListBoxData.Items.Refresh());
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using String = System.String;

namespace KAMTestStand;

public partial class LogIncomMessage : Window
{
    public LogIncomMessage(ObservableCollection<String> logIncomMessageList)
    {
        InitializeComponent();
        LogIncomMessageList = logIncomMessageList;
        ListBoxData.ItemsSource = LogIncomMessageList;
        BindingOperations.EnableCollectionSynchronization(LogIncomMessageList, ListBoxData);
    }

    public readonly ObservableCollection<String> LogIncomMessageList;
    
    private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
    {
        LogIncomMessageList.Clear();
        Dispatcher.Invoke(() => ListBoxData.Items.Refresh());
    }

    private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
    {
        Hide();
    }
    public void UpdateData()
    {
        //Dispatcher.Invoke(() => ListBoxData.Items.Refresh());
    }
}
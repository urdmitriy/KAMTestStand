using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace KAMTestStand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public delegate void ParseDataApp(string data);
    public delegate void MessagePrintApp(TextBlock textBlock, string message);
    public delegate void DataGridUpdateApp();

    public partial class MainWindow
    {
        private readonly Setting _settingsWindow;
        private readonly LogIncomMessage _logIncomingMessageWindow;
        private readonly ComData _comData;
        private readonly EntityList _entityList;

        public MainWindow()
        {
            _entityList = new EntityList(DataGridUpdate);
            var logIncomingMessageList = new List<string>();
            
            _logIncomingMessageWindow = new LogIncomMessage(logIncomingMessageList);

            var settings = new SettingsData();
            ReadSettingsFile(settings);
            _settingsWindow = new Setting(this, settings);
            
            _comData = new ComData(settings);
            _comData.Init();
            var tcpData = new TcpData(settings, logIncomingMessageList);
            var report = new Report(settings);

            var dataExchange = new DataExchange(_comData, tcpData, report, _entityList, logIncomingMessageList, 
                MessagePrint, _logIncomingMessageWindow.UpdateData, this);
            _comData.ParseDataAppSet(dataExchange.ParseData);
            
            InitializeComponent();
            DataGrid.ItemsSource = _entityList.Data;
            
            _entityList.AddDataEntity(new Entity(){SerialNumberVal = 123456789, Rs2321Res = ResultE.StatusPass, Rs2322Res = ResultE.StatusError});
            DataGridUpdate();
        }

        private void ButtonExit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSettings_OnClick(object sender, RoutedEventArgs e)
        {
            _settingsWindow.Show();
            Hide();
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            _comData.Deinit();
            _settingsWindow.Close();
            _logIncomingMessageWindow.Close();
        }

        private void MainWindow_OnContentRendered(object? sender, EventArgs e)
        {
            if (!_settingsWindow.SettingsIsValid())
            {
                Hide();
            }
        }

        private void ReadSettingsFile(SettingsData setting)
        {
            if (File.Exists("settings.txt"))
            {
                string?[] settings = File.ReadAllLines("settings.txt");

                setting.PortAxiName = settings[0];
                setting.PortDiscoveryName = settings[1];
                setting.PathReport = settings[2];
                int.TryParse(settings[3], out setting.PortSim);
            }
            else
            {
                Show();
            }
        }

        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            _entityList.Data.Clear();
            TextBlockMessage.Text = "";
            _logIncomingMessageWindow.LogIncomMessageList.Clear();
            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            Dispatcher.Invoke(()=> DataGrid.Items.Refresh()) ;
            if (DataGrid.Items.Count > 2)
                DataGrid.ScrollIntoView(DataGrid.Items[DataGrid.Items.Count - 1]);
        }
        
        private void MessagePrint(TextBlock textBlock, string message)
        {
            Dispatcher.Invoke(() => { textBlock.Text = message;});
        }
        
        private void ButtonIncomMessage_OnClick(object sender, RoutedEventArgs e)
        {
            _logIncomingMessageWindow.Show();
        }
    }
}

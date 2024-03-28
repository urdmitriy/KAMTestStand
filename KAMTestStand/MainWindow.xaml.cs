using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KAMTestStand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public delegate void ParseDataApp(string data);
    public delegate void MessagePrintApp(string data);
    public delegate void DataGridUpdateApp();

    public partial class MainWindow : Window
    {
        private readonly Setting _settingsWindow;
        private LogIncomMessage _logIncomMessageWindow;
        private readonly ComData _comData;
        private readonly EntityList _entityList;
        private List<String> _logIncomMessageList;

        public MainWindow()
        {
            _entityList = new EntityList(DataGridUpdate);
            _logIncomMessageList = new List<String>();
            
            _logIncomMessageWindow = new LogIncomMessage(_logIncomMessageList);

            var settings = new SettingsData();
            ReadSettingsFile(settings);
            _settingsWindow = new Setting(this, settings);
            
            _comData = new ComData(settings);
            _comData.Init();
            var tcpData = new TcpData(settings);
            var report = new Report(settings);

            var dataExchange = new DataExchange(_comData, tcpData, report, _entityList, _logIncomMessageList, 
                MessageInfoPrint, _logIncomMessageWindow.UpdateData);
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
            _logIncomMessageWindow.Close();
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
            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            Dispatcher.Invoke(()=>DataGrid.Items.Refresh()) ;
        }

        private void MessageInfoPrint(string message)
        {
            Dispatcher.Invoke(() => { TextBlockMessage.Text = message;});
        }

        private void ButtonIncomMessage_OnClick(object sender, RoutedEventArgs e)
        {
            _logIncomMessageWindow.Show();
        }
    }
}

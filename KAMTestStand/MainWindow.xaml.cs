using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public partial class MainWindow : Window
    {
        private readonly SettingsData _settings;
        readonly Setting _settingsWindow;
        public MainWindow()
        {
            var entityList = new EntityList();
            _settings = new SettingsData();
            ReadSettingsFile();
            _settingsWindow = new Setting(this, _settings);
            
            var _comData = new ComData(_settings);
            _comData.Init();
            var _tcpData = new TcpData(_settings);
            var _report = new Report(_settings);
            var _dataExchange = new DataExchange(_comData, _tcpData, _report);
            _comData.ParseDataAppSet(_dataExchange.ParseData);

            InitializeComponent();
            DataGrid.ItemsSource = entityList.Data;

            entityList.AddDataEntity(new Entity() { ModelId = 1, TimeReady = 12, SerialNumber = 787878 });
            entityList.AddDataEntity(new Entity() { TimeReady = 33, SerialNumber = 565656 });

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
            _settingsWindow.Close();
        }

        private void MainWindow_OnContentRendered(object? sender, EventArgs e)
        {
            if (!_settingsWindow.SettingsIsValid())
            {
                Hide();
            }
        }

        public void ReadSettingsFile()
        {
            if (File.Exists("settings.txt"))
            {
                string?[] settings = File.ReadAllLines("settings.txt");

                _settings.PortAxiName = settings[0];
                _settings.PortDiscoveryName = settings[1];
                _settings.PathReport = settings[2];
                int.TryParse(settings[3], out _settings.MaxCurrentDeepSleep);
                int.TryParse(settings[4], out _settings.MaxCurrentGsm);
                int.TryParse(settings[5], out _settings.MaxCurrentPeak);
                int.TryParse(settings[6], out _settings.MaxCurrentGsmSleep);
                int.TryParse(settings[7], out _settings.MaxTimeReady);
                int.TryParse(settings[8], out _settings.PortSim);
            }
            else
            {
                Show();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainWindow : Window
    {
        private SettingsData settingsApp;
        Setting settingsWindow;
        public MainWindow()
        {
            var entityList = new EntityList();
            settingsApp = new SettingsData();
            settingsWindow = new Setting(this, settingsApp);
            settingsWindow.ReadSettingsFile();

            InitializeComponent();
            DataGrid.ItemsSource = entityList.Data;
            
            entityList.AddDataEntity(new Entity(){ModelId = 1, TimeReady = 12, SerialNumber = 787878});
            entityList.AddDataEntity(new Entity(){TimeReady = 33, SerialNumber = 565656});
            
        }

        private void ButtonExit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSettings_OnClick(object sender, RoutedEventArgs e)
        {
            settingsWindow.Show();
            Hide();
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            settingsWindow.Close();
        }

        private void MainWindow_OnContentRendered(object? sender, EventArgs e)
        {
            if (!settingsWindow.SettingsIsValid())
            {
                Hide();
            }
        }
    }
}
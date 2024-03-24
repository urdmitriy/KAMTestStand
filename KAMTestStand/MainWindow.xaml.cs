using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            var entityList = new EntityList();
            entityList.ModifyEntity(new Entity(){ModelId = 1, TimeReady = 12, SerialNumber = 787878});
            entityList.ModifyEntity(new Entity(){TimeReady = 33, SerialNumber = 565656});

            InitializeComponent();
            DataGrid.ItemsSource = entityList.Data;
            
        }

        private void ButtonExit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSettings_OnClick(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new Setting(this);
            settingsWindow.Show();
            Hide();
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace KAMTestStand;

public partial class Setting : Window
{
    public Setting(Window parent, SettingsData settings)
    {
        InitializeComponent();
        _parent = parent;
        _settings = settings;
        
        if (SettingsIsValid())
        {
            TextBoxComAxi.Text = _settings.PortAxiName;
            TextBoxComDiscovery.Text = _settings.PortDiscoveryName;
            TextBoxPath.Text = _settings.PathReport;
            TextBoxPortSim.Text = _settings.PortSim.ToString();
        }

    }

    private readonly Window _parent;
    private readonly SettingsData _settings;
    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        var result = SettingsIsValid();
        if (!result) return;
        _parent.Show();
        Hide();
    }

    private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
    {
        _settings.PortAxiName = TextBoxComAxi.Text;
        _settings.PortDiscoveryName = TextBoxComDiscovery.Text;
        _settings.PathReport = TextBoxPath.Text;
        int.TryParse(TextBoxPortSim.Text, out _settings.PortSim);
        
        var result = SaveSettingsFile();
        if (!result) return;
        _parent.Show();
        Hide();
    }

    private bool SaveSettingsFile()
    {
        if (SettingsIsValid())
        {
            string?[] settings = new[]
            {
                _settings.PortAxiName,
                _settings.PortDiscoveryName,
                _settings.PathReport,
                _settings.PortSim.ToString(),
            };
            File.WriteAllLines("settings.txt", settings!);
            return true;
        }
        else
        {
            MessageBox.Show("Не заполнены все параметры настроек");
            return false;
        }
    }

    public bool SettingsIsValid()
    {
        if (string.IsNullOrEmpty(_settings.PortAxiName) ||
            string.IsNullOrEmpty(_settings.PortDiscoveryName) ||
            string.IsNullOrEmpty(_settings.PathReport) ||
            _settings.PortSim == 0)
        {
            return false;
        }

        return true;
    }
}
﻿using System.Collections.Generic;
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
    }

    private readonly Window _parent;
    private SettingsData _settings;
    
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
        int.TryParse(TextBoxIsleep.Text, out _settings.MaxCurrentDeepSleep);
        int.TryParse(TextBoxIgsm.Text, out _settings.MaxCurrentGsm);
        int.TryParse(TextBoxIpeack.Text, out _settings.MaxCurrentPeak);
        int.TryParse(TextBoxIsleepGsm.Text, out _settings.MaxCurrentGsmSleep);
        int.TryParse(TextBoxTimeOn.Text, out _settings.MaxTimeReady);
        int.TryParse(TextBoxPortSim.Text, out _settings.PortSim);
        
        var result = SaveSettingsFile();
        if (!result) return;
        _parent.Show();
        Hide();
    }
    
    public void ReadSettingsFile()
    {
        if (File.Exists("settings.txt"))
        {
            string?[] settings = File.ReadAllLines("settings.txt");

            _settings.PortAxiName = settings[0];
            _settings.PortDiscoveryName =settings[1];
            _settings.PathReport = settings[2];
            int.TryParse(settings[3], out _settings.MaxCurrentDeepSleep);
            int.TryParse(settings[4], out _settings.MaxCurrentGsm);
            int.TryParse(settings[5], out _settings.MaxCurrentPeak);
            int.TryParse(settings[6], out _settings.MaxCurrentGsmSleep);
            int.TryParse(settings[7], out _settings.MaxTimeReady);
            int.TryParse(settings[8], out _settings.PortSim);

            TextBoxComAxi.Text = _settings.PortAxiName;
            TextBoxComDiscovery.Text = _settings.PortDiscoveryName;
            TextBoxPath.Text = _settings.PathReport;
            TextBoxIsleep.Text = _settings.MaxCurrentDeepSleep.ToString();
            TextBoxIgsm.Text = _settings.MaxCurrentGsm.ToString();
            TextBoxIpeack.Text = _settings.MaxCurrentPeak.ToString();
            TextBoxIsleepGsm.Text = _settings.MaxCurrentGsmSleep.ToString();
            TextBoxTimeOn.Text = _settings.MaxTimeReady.ToString();
            TextBoxPortSim.Text = _settings.PortSim.ToString();
        }
        else
        {
            Show();
        }
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
                _settings.MaxCurrentDeepSleep.ToString(),
                _settings.MaxCurrentGsm.ToString(),
                _settings.MaxCurrentPeak.ToString(),
                _settings.MaxCurrentGsmSleep.ToString(),
                _settings.MaxTimeReady.ToString(),
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
            _settings.MaxCurrentDeepSleep == 0 ||
            _settings.MaxCurrentGsm == 0 ||
            _settings.MaxCurrentPeak == 0 ||
            _settings.MaxCurrentGsmSleep == 0 ||
            _settings.MaxTimeReady == 0 ||
            _settings.PortSim == 0)
        {
            return false;
        }

        return true;
    }
}
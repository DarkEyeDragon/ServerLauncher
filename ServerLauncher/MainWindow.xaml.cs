using Microsoft.Win32;
using ServerLauncher.Client;
using ServerLauncher.Server;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ServerLauncher.Config;

namespace ServerLauncher
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public bool Debug { get; set; }
        public JavaServer JavaServer { get; set; }
        private string _ramUsagePercent;
        public string RamUsagePercent
        {
            get => _ramUsagePercent;
            set
            {
                if (_ramUsagePercent == value) return;
                _ramUsagePercent = value;
                OnPropertyChanged();
            }
        }

        private string _ramUsageMegabyte;
        public string RamUsageMegabyte
        {
            get => _ramUsageMegabyte;
            set
            {
                if (_ramUsageMegabyte == value) return;
                _ramUsageMegabyte = value;
                OnPropertyChanged();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ConfigHandler.Load();
            Settings.Load();
            JavaServer = new JavaServer(inputXMS.Text, inputXMX.Text);
            OutputHandler.Log("Ready for launch!");
            if (Settings.AutoStart)
                JavaServer.Start();
            if (Settings.Debug)
                Debug = true;
        }


        private void ConsoleInputBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                

                if (ConsoleInputBox.Text.ToLower().Equals("start"))
                {
                    try
                    {
                        JavaServer = new JavaServer(inputXMS.Text, inputXMX.Text);
                        JavaServer.Start();
                    }
                    catch (NullReferenceException ex)
                    {
                        OutputHandler.Log(ex.ToString(), Level.ERROR);
                    }
                }  
                else if (ConsoleInputBox.Text.ToLower().Equals("stop"))
                    JavaServer.Stop();
                else if (ConsoleInputBox.Text.ToLower().Equals("restart"))
                {
                    try
                    {
                        JavaServer.Stop();
                        JavaServer = new JavaServer(inputXMS.Text, inputXMX.Text);
                        JavaServer.Start();
                    }
                    catch (NullReferenceException ex)
                    {
                        OutputHandler.Log(ex.ToString(), Level.ERROR);
                    }
                }
                else
                    JavaServer.Input(ConsoleInputBox.Text);
                ConsoleInputBox.Text = "";
            }
            
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            ConfigHandler.Config.Contents.Clear();
            ConfigHandler.Config.Contents.Add("xms", inputXMS.Text);
            ConfigHandler.Config.Contents.Add("xmx", inputXMX.Text);
            ConfigHandler.Config.Contents.Add("serverJarPath", inputJarLocation.Text);
            ConfigHandler.Config.Contents.Add("autoStart", checkboxAutoStart.IsChecked.ToString());
            ConfigHandler.Config.Contents.Add("debug", checkboxDebug.IsChecked.ToString());
            ConfigHandler.Save();
            ConfigHandler.Load();
            Debug = (bool)checkboxDebug.IsChecked;


        }

        private void btnJarLocation_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                inputJarLocation.Text = openFileDialog.FileName;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            e.Cancel = true;
            CommandExecutor.Command("stop");
            e.Cancel = false;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

using Microsoft.Win32;
using ServerLauncher.Client;
using ServerLauncher.Server;
using System;
using System.Windows;
using System.Windows.Input;

namespace ServerLauncher
{
    public partial class MainWindow : Window
    {
        public bool Debug { get; set; }
        public JavaServer JavaServer { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = JavaServer;
            ConfigHandler.Create();
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
            String[] startupSettings = { inputXMS.Text, inputXMX.Text, inputJarLocation.Text, checkboxAutoStart.IsChecked.ToString(), checkboxDebug.IsChecked.ToString() };
            ConfigItem configItem = new ConfigItem { Name = "Startup", Items = startupSettings };
            ConfigHandler.Add(configItem);
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
    }
}

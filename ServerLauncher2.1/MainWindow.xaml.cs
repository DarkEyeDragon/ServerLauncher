using Microsoft.Win32;
using ServerLauncher.Client;
using ServerLauncher.Server;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ServerLauncher
{
    public partial class MainWindow : Window
    {
        JavaServer javaServer;
        public MainWindow()
        {
            InitializeComponent();
            ConfigHandler.Create();
            ConfigHandler.Load();
            Settings.Load(this);
            javaServer = new JavaServer(inputXMS.Text, inputXMX.Text);
            //javaServer.Start();
        }


        private void ConsoleInputBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                

                if (ConsoleInputBox.Text.ToLower().Equals("start"))
                {
                    try
                    {
                        

                        javaServer = new JavaServer(inputXMS.Text, inputXMX.Text);
                        javaServer.Start();
                    }
                    catch (NullReferenceException ex)
                    {
                        OutputHandler.Log(ex.ToString(), Level.ERROR);
                    }
                }  
                else if (ConsoleInputBox.Text.ToLower().Equals("stop"))
                    javaServer.Stop();
                else
                    javaServer.Input(ConsoleInputBox.Text);
                ConsoleInputBox.Text = "";
            }
            
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            String[] startupSettings = { inputXMS.Text, inputXMX.Text};
            ConfigItem configItem = new ConfigItem { Name = "Startup", Items = startupSettings };
            ConfigHandler.Add(configItem);
            ConfigHandler.Load();
        }

        private void btnJarLocation_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                inputJarLocation.Text = openFileDialog.FileName;
                ConfigItem jarLocation = new ConfigItem { Name = "JarLocation", Item = openFileDialog.FileName };
            }
        }
    }
}

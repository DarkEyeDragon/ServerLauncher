using ServerLauncher.Client;
using ServerLauncher.Server;
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

namespace ServerLauncher
{
    public partial class MainWindow : Window
    {
        JavaServer paper;
        public MainWindow()
        {
            InitializeComponent();
            ConfigHandler.Create();
            ConfigHandler.Load();
            Settings.Load(this);
            paper = new JavaServer(ConfigHandler.Startup.Items[0], ConfigHandler.Startup.Items[1], "paper/PaperSpigot-latest.jar", this);
            paper.Start();
        }

        private void ConsoleInputBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ConsoleInputBox.Text.ToLower().Equals("start"))
                {
                    if (paper.ServerProcess.HasExited)
                    {
                        ConfigHandler.Load();
                        paper = new JavaServer(ConfigHandler.Startup.Items[0], ConfigHandler.Startup.Items[1], "paper/PaperSpigot-latest.jar", this);
                        paper.Start();
                    }
                    else
                    {
                        paper.Output("Server still running!");
                    }
                }  
                else if (ConsoleInputBox.Text.ToLower().Equals("stop"))
                    paper.Stop();
                else
                    paper.Input(ConsoleInputBox.Text);
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
    }
}

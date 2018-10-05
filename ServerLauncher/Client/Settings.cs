using System;
using System.Collections.Generic;
using System.Windows;
using ServerLauncher.Config;

namespace ServerLauncher.Client
{
    class Settings
    {

        public static readonly string default_XMS = "256M";
        public static readonly string default_XMX = "1G";
        public static readonly string default_JarLocation = "serverdir/server.jar";

        public static string Xms { get; set; }
        public static string Xmx { get; set; }
        public static string ServerJarPath { get; set; }
        public static bool AutoStart { get; set; }
        public static bool Debug { get; set; }

        public static void Load()
        {
            try
            {
                MainWindow main = ((MainWindow)(Application.Current.MainWindow));
                string xms = ConfigHandler.Config.Contents["xms"];
                string xmx = ConfigHandler.Config.Contents["xmx"];
                string serverJarPath = ConfigHandler.Config.Contents["serverJarPath"];
                bool autoStart = Convert.ToBoolean(ConfigHandler.Config.Contents["autoStart"]);
                main.inputXMS.Text = xms;
                main.inputXMX.Text = xmx;
                main.inputJarLocation.Text = serverJarPath;
                main.checkboxAutoStart.IsChecked = autoStart;
                Xms = xms;
                Xmx = xmx;
                ServerJarPath = serverJarPath;
                AutoStart = autoStart;

            }
            catch (Exception ex)
            {
                if(ex is KeyNotFoundException || ex is NullReferenceException)
                {
                    ConfigHandler.SetDefault();
                }
                else
                {
                    OutputHandler.Log(ex.ToString(), Level.ERROR);
                    OutputHandler.Log("This is an unhandled Exception. Please report this issue on https://github.com/DarkEyeDragon/ServerLauncher/issues", Level.ERROR);
                }
            }
        }
        public static void SetToDefault()
        {
            MainWindow main = ((MainWindow)(Application.Current.MainWindow));
            main.inputJarLocation.Text = default_JarLocation;
            main.inputXMS.Text = default_XMS;
            main.inputXMX.Text = default_XMX;
            main.checkboxAutoStart.IsChecked = false;
            main.checkboxDebug.IsChecked = false;
        }
    }
}

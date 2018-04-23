using System;
using System.Windows;

namespace ServerLauncher.Client
{
    class Settings
    {

        public static readonly string default_XMS = "256M";
        public static readonly string default_XMX = "1G";
        public static readonly string default_JarLocation = "serverdir/server.jar";
        public static readonly string autoStart = "false";

        public static string Xms { get; set; }
        public static string Xmx { get; set; }
        public static string ServerJarPath { get; set; }
        public static bool AutoStart { get; set; }

        public static void Load(MainWindow main)
        {
            try
            {
                string xms = ConfigHandler.Startup.Items[0];
                string xmx = ConfigHandler.Startup.Items[1];
                string serverJarPath = ConfigHandler.Startup.Items[2];
                bool autoStart = Convert.ToBoolean(ConfigHandler.Startup.Items[3]);
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
                if(ex is NullReferenceException)
                {
                    ConfigHandler.SetDefault();
                }
                else
                {
                    OutputHandler.Log(ex.ToString(), Level.ERROR);
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
        }
    }
}

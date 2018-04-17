using System;
using System.Windows;

namespace ServerLauncher.Client
{
    class Settings
    {

        private static readonly string default_XMS = "256M";
        private static readonly string default_XMX = "1G";
        private static readonly string default_JarLocation = "serverdir/server.jar";


        public static void Load(MainWindow main)
        {
            try
            {
                string xms = ConfigHandler.Startup.Items[0];
                string xmx = ConfigHandler.Startup.Items[1];
                main.inputXMS.Text = xms;
                main.inputXMX.Text = xmx;
            }
            catch (NullReferenceException)
            {
                ConfigHandler.SetDefault();
            }
        }
        public static void SetToDefault()
        {
            MainWindow main = ((MainWindow)(Application.Current.MainWindow));
            main.inputJarLocation.Text = default_JarLocation;
            main.inputXMS.Text = default_XMS;
            main.inputXMX.Text = default_XMX;
        }
    }
}

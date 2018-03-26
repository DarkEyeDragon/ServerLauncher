using ServerLauncher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLauncher.Client
{
    static class Settings
    {

        public static void Load(MainWindow main)
        {
            string xms = ConfigHandler.Startup.Items[0];
            string xmx = ConfigHandler.Startup.Items[1];
            main.inputXMS.Text = xms;
            main.inputXMX.Text = xmx;
        }
    }
}

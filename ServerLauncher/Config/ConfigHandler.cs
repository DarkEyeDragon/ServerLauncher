using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ServerLauncher.Client;

namespace ServerLauncher.Config
{
    class ConfigHandler
    {

        private static readonly string location = Directory.GetCurrentDirectory();
        private static readonly string file = Path.Combine(location, "config.json");

        public static Config Config { get; set; }

        static ConfigHandler()
        {
            Config = new Config();
        }

        public static void Load()
        {
            if (File.Exists(file))
            {
                Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(file));
            }
            else
            {
                OutputHandler.Log("Unable to load config file! Server will not start.", Level.ERROR);
            }
            
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Config, Formatting.Indented);
            File.WriteAllText(file, json);
        }

        public void Create(bool overwrite)
        {
            if (!overwrite)
            {
                if (!File.Exists(file))
                {
                    File.Create(file);
                }
            }
            else
            {
                File.Delete(file);
                File.Create(file);
            }
        }

        public static void SetDefault()
        {
            OutputHandler.Log("Config has been reset", Level.WARN);
            Config.Contents.Clear();
            Config.Contents.Add("xms", "500M");
            Config.Contents.Add("xmx", "500M");
            Config.Contents.Add("serverJarPath", "");
            Config.Contents.Add("autoStart", "false");
            Save();
            Settings.Load();
        }

    }
}

using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace ServerLauncher.Client
{
    static class ConfigHandler
    {
        private static readonly string location = Directory.GetCurrentDirectory();
        private static readonly string file = Path.Combine(location, "config.json");
        public static ConfigItem Startup  { get; set; }

        public static void Create()
        {
            if (!File.Exists(file))
            {
                File.Create(file);
            }
        }
        public static void Create(bool writeOver)
        {
            if (!writeOver) { 
                if (!File.Exists(file))
                {
                    File.Create(file);
                    SetDefault();
                    Save();
                }
            }
            else
            {
                File.Delete(file);
                File.Create(file);
                SetDefault();
                Save();
            }
        }
        public static void Load()
        {
            try
            {
                ConfigItem json = JsonConvert.DeserializeObject<ConfigItem>(File.ReadAllText(file));
                Startup = json;
            }
            catch (Exception ex)
            {
                OutputHandler.Log(ex.Message);
            }
        }
        public static void Add(ConfigItem configItem)
        {
            string json = JsonConvert.SerializeObject(configItem);
            File.WriteAllText(file, json);
        }

        public static void SetDefault()
        {
            Settings.SetToDefault();
            Save();
        }
        public static void Save()
        {
            MainWindow main = ((MainWindow)(Application.Current.MainWindow));
            String[] startupSettings = { main.inputXMS.Text, main.inputXMX.Text, main.inputJarLocation.Text, main.checkboxAutoStart.IsChecked.ToString()};
            ConfigItem configItem = new ConfigItem { Name = "Startup", Items = startupSettings };
            Add(configItem);
            Load();
        }
    }
}

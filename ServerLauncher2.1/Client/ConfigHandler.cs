﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace ServerLauncher.Client
{
    static class ConfigHandler
    {
        private static readonly string location = ;
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
                }
            }
            else
            {
                File.Delete(file);
                File.Create(file);
            }
        }
        public static void Load()
        {
            ConfigItem json = JsonConvert.DeserializeObject<ConfigItem>(File.ReadAllText(file));
            Startup = json;
        }
        public static void Add(ConfigItem configItem)
        {
            string json = JsonConvert.SerializeObject(configItem);
            File.WriteAllText(file, json);
        }
    }
}

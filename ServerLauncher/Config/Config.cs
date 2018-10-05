using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLauncher.Config
{
    class Config
    {
        public Dictionary<string, string> Contents { get; set; }

        public Config()
        {
            Contents = new Dictionary<string, string>();
        }
    }
}

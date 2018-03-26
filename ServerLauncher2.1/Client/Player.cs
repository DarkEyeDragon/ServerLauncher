using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLauncher.Client
{
    class Player
    {
        public string Username { get; set; }
        public string UUID { get; set; }
        public string IP { get; set; }

        public override string ToString()
        {
            return $"Username: {Username}, UUID: {UUID}, IP: {IP}";
        }

    }
}

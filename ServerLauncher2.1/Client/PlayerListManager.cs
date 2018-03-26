using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLauncher.Client
{
    static class PlayerListManager
    {
        public static List<Player> PlayerList = new List<Player>();
        public static void Remove(string username)
        {
            PlayerList = PlayerList.Where(p => p.Username != username).ToList();
        }
    }
}

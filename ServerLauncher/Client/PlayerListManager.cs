using ServerLauncher.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ServerLauncher.Client
{
    class PlayerListManager
    {

        public static List<Player> PlayerList = new List<Player>();
        private static List<Label> DisplayLabels = new List<Label>();
        private static List<Player> RemoveFromList = new List<Player>();



        public static void Add(Player player, MainWindow main)
        {
            PlayerList.Add(player);
            Display(main);
        }
        public static void Remove(string playerName, MainWindow main)
        {
            
            foreach (Player player in PlayerList)
            {
                if (player.Username.Equals(playerName))
                {
                    RemoveFromList.Add(player);
                }
            }
            foreach (Player player in RemoveFromList)
            {
                PlayerList.Remove(player);
            }
            Display(main);
        }
        public static void Remove(Player player, MainWindow main)
        {
            PlayerList.Remove(player);
            Display(main);
        }
        public static void Display(MainWindow window)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window.PlayerListGrid.Children.Clear();
                int index = 1;
                foreach (Player player in PlayerList)
                {
                    Label username = new Label { Content = player.Username, Name = $"player_{player.Username}" };
                    Grid.SetRow(username, index);
                    Grid.SetColumn(username, 1);
                    window.PlayerListGrid.Children.Add(username);

                    Button ban = new Button { Content = "Ban", Name = $"action_{player.Username}", Margin = new Thickness(2, 2, 2, 2) };
                    Button kick = new Button { Content = "Kick", Name = $"action_{player.Username}", Margin = new Thickness(2, 2, 2, 2) };
                    Button mute = new Button { Content = "Mute", Name = $"action_{player.Username}", Margin = new Thickness(2, 2, 2, 2) };

                    ban.Click += (sender, args) => { CommandExecutor.Command($"ban {player.Username}"); };
                    kick.Click += (sender, args) => { CommandExecutor.Command($"kick {player.Username}"); };
                    mute.Click += (sender, args) => { CommandExecutor.Command($"mute {player.Username}"); };

                    Grid.SetRow(ban, index);
                    Grid.SetColumn(ban, 2);

                    Grid.SetRow(kick, index);
                    Grid.SetColumn(kick, 3);

                    Grid.SetRow(mute, index);
                    Grid.SetColumn(mute, 4);

                    window.PlayerListGrid.Children.Add(ban);
                    window.PlayerListGrid.Children.Add(kick);
                    window.PlayerListGrid.Children.Add(mute);
                    index++;
                }
            }));
        }
        protected static void Ban(String username)
        {
            CommandExecutor.Command("/ban " + username);
        }
    }
}

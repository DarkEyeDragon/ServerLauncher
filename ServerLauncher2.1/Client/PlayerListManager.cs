using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ServerLauncher.Client
{
    class PlayerListManager
    {
        public static List<Player> PlayerList = new List<Player>();
        private static List<Label> PlayerListLables = new List<Label>(); 
        public static void Remove(string username, MainWindow mainWin)
        {
            PlayerList = PlayerList.Where(p => p.Username != username).ToList();
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                foreach (UIElement ui in mainWin.PlayerListGrid.Children.Cast<UIElement>().Where(p => p is Label && ((Label)p).Content.ToString() == username).ToList())
                {
                    mainWin.PlayerListGrid.Children.Remove(ui);
                }
            }));
        }

        public static void Update()
        {
            PlayerListLables.Clear();
            for (int i = 0; i < PlayerList.Count; i++)
            {
                Label username = new Label { Margin = new Thickness(20, i * 20, 0, 0), Content = PlayerList[i].Username };
                PlayerListLables.Add(username);
            }
        }

        public static void Display(MainWindow window)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window.PlayerListGrid.Children.Clear();
                Update();
                foreach (var label in PlayerListLables)
                {
                    window.PlayerListGrid.Children.Add(label);
                }
            }));
        }
    }
}

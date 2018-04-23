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
        private static List<Label> PlayerListLables = new List<Label>();
        private static List<Button> PlayerListButtons = new List<Button>();
        public static void Remove(string username, MainWindow mainWin)
        {
            PlayerList = PlayerList.Where(p => p.Username != username).ToList();
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                foreach (UIElement ui in mainWin.PlayerListGrid.Children.Cast<UIElement>().Where(label => label is Label && ((Label)label).Name == username).ToList())
                {
                    mainWin.PlayerListGrid.Children.Remove(ui);
                }
                foreach (UIElement ui in mainWin.PlayerListGrid.Children.Cast<UIElement>().Where(button => button is Button && ((Button)button).Name == username).ToList())
                {
                    mainWin.PlayerListGrid.Children.Remove(ui);
                }
            }));
        }

        public static void Update()
        {
            PlayerListLables.Clear();
            PlayerListButtons.Clear();
            for (int i = 0; i < PlayerList.Count; i++)
            {
                Label username = new Label { Margin = new Thickness(20, i * 20, 0, 0), Content= PlayerList[i].Username, Name = PlayerList[i].Username };
                Button action = new Button { Margin = new Thickness(40, i * 20, 0, 0), Content = "Actions...", Name=PlayerList[i].Username, Height=20, Width=20 };
                PlayerListLables.Add(username);
                PlayerListButtons.Add(action);
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
                foreach (var button in PlayerListButtons)
                {
                    window.PlayerListGrid.Children.Add(button);
                }
            }));
        }

        //string test = $"this is a test if {if(true) "bread" else "not bread"}";
    }
}

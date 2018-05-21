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
        private static List<UIElement> RemoveFromList = new List<UIElement>();


        public static void Remove(string username, MainWindow mainWin)
        {

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {

                foreach (UIElement uiElement in mainWin.PlayerListGrid.Children)
                {
                    if (uiElement is Label label)
                    {
                        if (label.Name.Contains("player_"))
                        {
                            RemoveFromList.Add(label);
                        }
                    }
                    else
                    {
                        if (uiElement is Button button)
                        {
                            if (button.Name.Contains("action_"))
                            {
                                RemoveFromList.Add(button);
                            }
                        }
                    }
                }
                foreach (UIElement item in RemoveFromList)
                {
                    mainWin.PlayerListGrid.Children.Remove(item);
                }
                PlayerList = PlayerList.Where(p => p.Username != username).ToList();

                /*foreach (UIElement ui in mainWin.PlayerListGrid.Children.Cast<UIElement>().Where(label => label is Label && ((Label)label).Name == username).ToList())
                {
                    mainWin.PlayerListGrid.Children.Remove(ui);
                }
                foreach (UIElement ui in mainWin.PlayerListGrid.Children.Cast<UIElement>().Where(button => button is Button && ((Button)button).Name == username).ToList())
                {
                    mainWin.PlayerListGrid.Children.Remove(ui);
                }
                foreach (UIElement ui in mainWin.PlayerListGrid.Children.Cast<UIElement>().Where(button => button is Button && ((Button)button).Name == "action").ToList())
                {
                    mainWin.PlayerListGrid.Children.Remove(ui);
                }*/
            }));
        }

        public static void Display(MainWindow window)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 0; i < PlayerList.Count; i++)
                {
                    int _count = PlayerList.Count;
                    if (_count > 0)
                    {
                        Label username = new Label { Content = PlayerList[_count - 1].Username, Name = $"player_{PlayerList[_count - 1].Username}" };
                        Grid.SetRow(username, PlayerList.Count);
                        Grid.SetColumn(username, 1);
                        window.PlayerListGrid.Children.Add(username);

                        Button ban = new Button { Content = "Ban", Name = $"action_{PlayerList[_count - 1].Username}", Margin = new Thickness(2, 2, 2, 2) };
                        Button kick = new Button { Content = "Kick", Name = $"action_{PlayerList[_count - 1].Username}", Margin = new Thickness(2, 2, 2, 2) };
                        Button mute = new Button { Content = "Mute", Name = $"action_{PlayerList[_count - 1].Username}", Margin = new Thickness(2, 2, 2, 2) };

                        ban.Click += (sender, args) => { CommandExecutor.Command($"ban {PlayerList[_count - 1].Username}"); };
                        kick.Click += (sender, args) => { CommandExecutor.Command($"kick {PlayerList[_count - 1].Username}"); };
                        mute.Click += (sender, args) => { CommandExecutor.Command($"mute {PlayerList[_count - 1].Username}"); };

                        Grid.SetRow(ban, PlayerList.Count);
                        Grid.SetColumn(ban, 2);

                        Grid.SetRow(kick, PlayerList.Count);
                        Grid.SetColumn(kick, 3);

                        Grid.SetRow(mute, PlayerList.Count);
                        Grid.SetColumn(mute, 4);

                        window.PlayerListGrid.Children.Add(ban);
                        window.PlayerListGrid.Children.Add(kick);
                        window.PlayerListGrid.Children.Add(mute);
                    }
                }
            }));
        }
        protected static void Ban(String username)
        {
            CommandExecutor.Command("/ban " + username);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ServerLauncher.Client
{
    class PlayerListManager
    {
        public static List<Player> playerList = new List<Player>();
        private static List<Label> playerListLabels = new List<Label>();
        private static List<Button> actionsButtons = new List<Button>();
        public static void Remove(string username, MainWindow mainWin)
        {
            playerList = playerList.Where(p => p.Username != username).ToList();
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                foreach (UIElement ui in mainWin.PlayerListGrid.Children.Cast<UIElement>().Where(label => label is Label && ((Label)label).Name.ToString() == username).ToList())
                {
                    mainWin.PlayerListGrid.Children.Remove(ui);
                }
                foreach (UIElement ui in mainWin.PlayerListGrid.Children.Cast<UIElement>().Where(button => button is Button && ((Button)button).Name.ToString() == username).ToList())
                {
                    mainWin.PlayerListGrid.Children.Remove(ui);
                }
            }));
        }

        public static void Update()
        {
            playerListLabels.Clear();
            actionsButtons.Clear();
            for (int i = 0; i < playerList.Count; i++)
            {
                Label username = new Label { Margin = new Thickness(20, i * 20, 0, 0), Content = playerList[i].Username, Name=playerList[i].Username };
                Button actions = new Button { Margin = new Thickness(80, i * 20, 0, 0), Width=60, Height=30, Content = "Actions", Name = playerList[i].Username };
                playerListLabels.Add(username);
                actionsButtons.Add(actions);
            }
        }

        public static void Display(MainWindow window)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Grid grid = new Grid();
                grid.Width = 250;
                grid.Height = 100;
                grid.HorizontalAlignment = HorizontalAlignment.Left;
                grid.VerticalAlignment = VerticalAlignment.Top;
                grid.ShowGridLines = true;
                ColumnDefinition colDef1 = new ColumnDefinition();
                ColumnDefinition colDef2 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(colDef1);
                grid.ColumnDefinitions.Add(colDef2);

                for (int y = 0; y < playerList.Count; y++)
                {
                    RowDefinition rowDef = new RowDefinition();
                    grid.RowDefinitions.Add(rowDef);
                    Grid.SetRow(playerListLabels[y], y);
                    Grid.SetColumn(playerListLabels[y], 0);
                    Grid.SetRow(actionsButtons[y], y);
                    Grid.SetColumn(actionsButtons[y], 1);
                    grid.Children.Add(playerListLabels[y]);
                    grid.Children.Add(actionsButtons[y]);
                }

                //window.PlayerListGrid.Children.Clear();
                //Update();
                //foreach (var label in playerListLabels)
                //{
                //    window.PlayerListGrid.Children.Add(label);
                //}
                //foreach(var actionsButton in actionsButtons)
                //{
                //    window.PlayerListGrid.Children.Add(actionsButton);
                //}
            }));
        }
        void ShowMenu(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

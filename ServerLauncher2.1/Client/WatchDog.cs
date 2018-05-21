using ServerLauncher.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ServerLauncher.Client
{
    static class Watchdog
    {
        private static DispatcherTimer timer = new DispatcherTimer();

        public static bool running = false;
        private static int failedAttempts = 0;

        public static void Start()
        {
            timer.IsEnabled = true;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        public static void Stop()
        {
            timer.IsEnabled = false;
            timer.Stop();
        }


        private static void OnTimerTick(object sender, EventArgs e)
        {
            MainWindow main = ((MainWindow)Application.Current.MainWindow);
            if(main.Debug) OutputHandler.Log($"Running: {running} \r\n Failed attempts: {failedAttempts}");

            if (running)
                failedAttempts = 0;
            else
            {
                if (failedAttempts > 60)
                {
                    OutputHandler.Log("Server is idle... checking if it's still responding!", Level.WARN);
                    CommandExecutor.Command("list");
                }
                if (failedAttempts > 180)
                {
                    OutputHandler.Log("Server has not responded in a long time! Restarting...", Level.WARN);
                    main.JavaServer.Stop();
                    main.JavaServer.Start();
                    failedAttempts = 0;
                }
                else
                {
                    failedAttempts++;
                }
            }
            running = false;
        }
    }
}

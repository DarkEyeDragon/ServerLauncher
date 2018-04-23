using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ServerLauncher.Client
{

    enum Level {INFO, WARN, ERROR}
    static class OutputHandler
    {
        private static MainWindow main = ((MainWindow)(Application.Current.MainWindow));
        public static void Log(string output, Level level)
        {
            if (!string.IsNullOrEmpty(output))
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Run outputLine = new Run();
                    if (level == Level.INFO)
                        outputLine = new Run(output + Environment.NewLine);
                    else if (level == Level.WARN)
                        outputLine = new Run(output + Environment.NewLine) { Foreground = Brushes.Orange };
                    else if(level == Level.ERROR)
                        outputLine = new Run(output + Environment.NewLine) { Foreground = Brushes.Red };
                    main.outputstream.Inlines.Add(outputLine);
                    main.scrollviewer.ScrollToBottom();
                }));
            }
        }
        public static void Log(string output)
        {
            if (!string.IsNullOrEmpty(output))
            {
                main.Dispatcher.Invoke(new Action(() =>
                {
                    MainWindow main = ((MainWindow)Application.Current.MainWindow);
                    main.outputstream.Inlines.Add(new Run(output + Environment.NewLine));
                    main.scrollviewer.ScrollToBottom();
                }));
            }
        }
    }
}

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
        public static void Log(string output, Level level, MainWindow window)
        {
            var main = Application.Current;
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
                    window.outputstream.Inlines.Add(outputLine);
                    window.scrollviewer.ScrollToBottom();
                }));
            }
        }
        public static void Log(string output, MainWindow window)
        {
            if (!string.IsNullOrEmpty(output))
            {
                window.Dispatcher.Invoke(new Action(() =>
                {
                     window.outputstream.Inlines.Add(new Run(output + Environment.NewLine));
                    window.scrollviewer.ScrollToBottom();
                }));
            }
        }
        
    }
}

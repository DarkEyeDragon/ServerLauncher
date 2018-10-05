using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ServerLauncher.Client
{

    enum Level {INFO, WARN, ERROR}
    static class OutputHandler
    {
        static readonly MainWindow main = (MainWindow)(Application.Current.MainWindow);

        public static void Log(string output, Level level)
        {
            if (!string.IsNullOrEmpty(output) && main != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (main.outputstream.Inlines.Count > 100)
                    {
                        main.outputstream.Inlines.Remove(main.outputstream.Inlines.FirstInline);
                    }
                    Run outputLine = new Run();
                    if (level == Level.INFO)
                        outputLine = new Run(output + Environment.NewLine);
                    else if (level == Level.WARN)
                        outputLine = new Run(output + Environment.NewLine) { Foreground = Brushes.Orange };
                    else if(level == Level.ERROR)
                        outputLine = new Run(output + Environment.NewLine) { Foreground = Brushes.Red };
                    main.outputstream.Inlines.Add(outputLine);
                    main.scrollviewer.ScrollToBottom();
                });
            }
        }
        public static void Log(string output)
        {
            if (!string.IsNullOrEmpty(output))
            {
                main.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        if (main.outputstream.Inlines.Count > 100)
                        {
                            main.outputstream.Inlines.Remove(main.outputstream.Inlines.FirstInline);
                        }
                        if (main == null) return;
                        main.outputstream.Inlines.Add(new Run(output + Environment.NewLine));
                        main.scrollviewer.ScrollToBottom();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLineIf(main == null, e);
                    }
                    
                });
            }
        }
    }
}

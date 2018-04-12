using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerLauncher.Client
{
    class PopupMenu
    {
        private Window window;

        public string Title { get; set; }


        public PopupMenu()
        {
            window = new Window
            {
                Width = 500,
                Height = 300
            };
            window.Show();
        }

        public void AddToBody(object obj)
        {
            //if(obj is String)
        }
    }
}

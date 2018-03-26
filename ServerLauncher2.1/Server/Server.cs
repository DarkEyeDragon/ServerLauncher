using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLauncher.Server
{
    public interface IServer
    {
        Process ServerProcess { get;}
        void Start();
        void Stop();
        //void Output(string output);
        void Input(string input);
    }
}

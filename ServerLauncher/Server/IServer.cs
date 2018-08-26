using System.Diagnostics;

namespace ServerLauncher.Server
{
    public interface IServer
    {
        Process ServerProcess { get;}
        void Start();
        void Stop();
        void Output(string output);
        void Input(string input);
    }
}

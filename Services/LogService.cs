using System;
using System.IO;

namespace Tesonet
{
    class LogService : ILogService
    {
        public void Log(string message)
        {
            File.AppendAllText(Constants.LogFile, DateTime.Now + " " + message + Environment.NewLine);
        }
    }
}

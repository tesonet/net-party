using System;

namespace partycli.core.Logging
{
    public interface ILogger
    {
        void Debug(string message);
        void Error(string message);
        void Error(string message, Exception e);
        void Info(string message);
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace partycli.core.Logging
{
    public class Logger : ILogger
    {
        ILog _log;

        public Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
            _log = LogManager.GetLogger(GetType());
        }

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string message, Exception e)
        {
            _log.Error(message, e);
        }
    }
}

using log4net;
using System;
using System.Collections.Generic;

namespace partycli.Helpers
{
    class Printer : IPrinter
    {
        ILog m_log = null;
        public Printer(ILog log)
        {
            m_log = log;
        }
        public void ServersList(List<Server> servers)
        {
            foreach (var server in servers)
                m_log.Info("ServerName:" + server.Name + " " + "Distance:" + server.Distance);

            m_log.Info("That was all. Press any key to continue");
            Console.ReadKey();
        }

        public void Error(string error)
        {
            m_log.Error(error);
            m_log.Info("Press any key to continue");
            Console.ReadKey();
        }

        public void Info(string info)
        {
            m_log.Info(info);
        }
    }
}

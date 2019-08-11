using NLog;

namespace Net_party.Logging
{
    class NLogger
    {
        private static Logger _logger;

        public static Logger GetInstance()
        {
            if (_logger != null)
            {
                return _logger;
            }

            _logger = LogManager.GetCurrentClassLogger();
            return _logger;
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Net_party.Logging
{
    class ExceptionLogging
    {
        public static void CatchAndLogErrors(Action action, string customMessage = null, bool rethrow = true)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                LogMessage(customMessage ?? (object) ex);
                if (rethrow)
                {
                    throw;
                }
            }
        }

        public static async Task<T> CatchAndLogErrors<T>(Func<Task<T>> action, string customMessage = null, bool rethrow = true)
        {
            try
            {
                var value = await action.Invoke();
                return value;
            }
            catch (Exception ex)
            {
                LogMessage(customMessage ?? (object) ex);
                if (rethrow)
                {
                    throw;
                }

                return default;
            }
        }

        public static T CatchAndLogErrors<T>(Func<T> action, string customMessage = null, bool rethrow = true)
        {
            try
            {
                var value = action.Invoke();
                return value;
            }
            catch (Exception ex)
            {
                LogMessage(customMessage ?? (object)ex);
                if (rethrow)
                {
                    throw;
                }

                return default;
            }
        }

        private static void LogMessage(object message)
        {
            NLogger.GetInstance().Error(message);
        }
    }
}

using log4net;

namespace partycli.Models.Service
{
    public interface ILogProvider
    {
        ILog Get<T>(string v);
    }
}
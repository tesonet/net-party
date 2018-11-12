using System;
using PartyCli.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PartyCli.Infrastructure.Exeptions;

namespace PartyCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstraper.Configure();
            var logger = Bootstraper.Provider.GetService<ILogger>();
            var presenter = Bootstraper.Provider.GetService<IPresenter>();

            try
            {
                var receiver = Bootstraper.Provider.GetService<IReceiver>();

                receiver.Accept(args);
            }
            catch (PresentableExeption ex)
            {
                logger.Error(ex.ToString());
                presenter.DisplayMessage(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
    }
}

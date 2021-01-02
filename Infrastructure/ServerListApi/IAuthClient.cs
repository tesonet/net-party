namespace Tesonet.ServerListApp.Infrastructure.ServerListApi
{
    using System.Threading.Tasks;

    internal interface IAuthClient
    {
        /// <summary>
        /// Performs an authorization with the remote server.
        /// </summary>
        /// <returns>Authorization token, if successful.</returns>
        Task<string> Authorize();
    }
}

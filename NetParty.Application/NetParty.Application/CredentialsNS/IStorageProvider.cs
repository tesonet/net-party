#region Using

using System.IO;

#endregion

namespace NetParty.Application.CredentialsNS
    {
    public interface IStorageProvider
        {
        /// <summary>
        ///     Returns a stream which is used as storage
        /// </summary>
        /// <returns>Stream which the user is responsible for disposing</returns>
        Stream GetStorage();
        }
    }

// /*--------------------------------------------------------------------------------------+
// |
// |     $Source: ConstructionField/FieldEngineer/JsUIDesktopApp/FieldEngineerWpf.sln.DotSettings $
// |
// |  $Copyright: (c) 2018 Bentley Systems, Incorporated. All rights reserved. $
// |
// +--------------------------------------------------------------------------------------*/

#region Using

using System.Threading.Tasks;

#endregion

namespace NetParty.Application.CredentialsNS
    {
    public interface ICredentialsRepository
        {
        Task StoreAsync(Credentials credentials);
        Task<Credentials> LoadAsync();
        }
    }

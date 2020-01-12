using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_party.Services.Contracts
{
    public interface IPasswordService
    {
        string HashPassword(string password);

        bool ValidatePassword(string password, string correctHash);
    }
}

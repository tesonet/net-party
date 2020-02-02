using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli
{
    static class Constants
    {
        public static readonly string tokenUrl = "http://playground.tesonet.lt/v1/tokens";
        public static readonly string serverUrl = "http://playground.tesonet.lt/v1/servers";
    }

    static class ConfigConstants
    {
        public static readonly string log = "log";
        public static readonly string username = "username";
        public static readonly string password = "password";
        public static readonly string serverList = "serverlist";
    }

    static class Commands
    {
        public static readonly string username = "username";
        public static readonly string password = "password";
        public static readonly string local = "--local";
        public static readonly string config = "config";
        public static readonly string serverList = "server_list";
    }
}

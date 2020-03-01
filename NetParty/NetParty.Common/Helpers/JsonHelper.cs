using Newtonsoft.Json;

namespace NetParty.Common.Helpers
{
    public static class JsonHelper
    {
        public static string JsonToString(this object data)
        {
            if (data != null)
                return JsonConvert.SerializeObject(data);
            else
                return null;
        }
    }
}
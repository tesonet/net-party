using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetParty.Application.Utils
{
    public static class ObjectExtentions
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}

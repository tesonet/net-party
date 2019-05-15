using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetParty.Utils
{
    public static class ObjectByteArrayExtensions
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        public static T ToObject<T>(this byte[] bytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(bytes, 0, bytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            return (T) binForm.Deserialize(memStream);
        }
    }
}
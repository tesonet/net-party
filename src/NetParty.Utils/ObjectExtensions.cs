using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace NetParty.Utils
{
    public static class ObjectExtensions
    {
        private static readonly JsonSerializer Serializer;
        static ObjectExtensions()
        {
            Serializer = JsonSerializer.Create();
        }

        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
                return null;

            MemoryStream ms = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(ms))
            {
                Serializer.Serialize(writer, obj);
            }

            return ms.ToArray();
        }

        public static T ToObjectType<T>(this byte[] data)
            where T : class
        {
            if (data == null)
                return null;

            using (var stream = new MemoryStream(data))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
                return Serializer.Deserialize(reader, typeof(T)) as T;
        }
    }
}

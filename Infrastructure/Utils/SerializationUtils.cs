/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Framework.Infrastructure.Utils
{
    public static class SerializationUtils
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
            {
                return (byte[])null;
            }

            var binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static T FromByteArray<T>(this byte[] byteArray)
            where T : class
        {
            if (byteArray == null)
            {
                return default(T);
            }

            var binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                return binaryFormatter.Deserialize(memoryStream) as T;
            }
        }
    }
}

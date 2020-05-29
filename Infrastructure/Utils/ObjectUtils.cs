/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.IO;
using System.Xml.Serialization;

namespace Framework.Infrastructure.Utils
{
    public static class ObjectUtils
    {
        public static T DeepCopy<T>(T object2Copy)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(T));

                serializer.Serialize(stream, object2Copy);
                stream.Position = 0;
                T objectCopy = (T)serializer.Deserialize(stream);

                return objectCopy;
            }
        }
    }
}

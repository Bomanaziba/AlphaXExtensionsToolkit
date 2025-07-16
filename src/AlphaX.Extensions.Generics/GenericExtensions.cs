using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace AlphaX.Extensions.Generics
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Gets generic type name.
        /// </summary>
        /// <param name="type">The type.</param>
        public static string GetGenericTypeName(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type), "Type cannot be null.");

            string typeName;

            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }

        /// <summary>
        /// Gets generic type name.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public static string GetGenericTypeName(this object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj), "Object cannot be null.");

            return obj.GetType().GetGenericTypeName();
        }

        /// <summary>
        /// Objects to byte array.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <typeparam name="T"></typeparam>
        public static byte[] ObjectToByteArray<T>(this T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj), "Object cannot be null.");

            var json = JsonConvert.SerializeObject(obj);
            return System.Text.Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Bytes array to object.
        /// </summary>
        /// <param name="arrBytes">The arr bytes.</param>
        /// <typeparam name="T"></typeparam>
        public static T ByteArrayToObject<T>(this byte[] arrBytes)
        {
            if (arrBytes == null || arrBytes.Length == 0)
                throw new ArgumentNullException(nameof(arrBytes), "Byte array cannot be null or empty.");

            var json = System.Text.Encoding.UTF8.GetString(arrBytes);
            return JsonConvert.DeserializeObject<T>(json);
        }


                /// <summary>
        /// Gets object prop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static PropertyInfo[] GetObjectProp<T>(this T parameter)
        {
            PropertyInfo[] propertyInfos;

            propertyInfos = typeof(T).GetProperties();

            Array.Sort(propertyInfos,
            delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            return propertyInfos;
        }
    }
}

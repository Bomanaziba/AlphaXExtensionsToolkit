
using System;
using System.IO;
using System.Xml.Serialization;

namespace AlphaX.Extensions.String
{
    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// Froms hex string to base64 string.
        /// </summary>
        /// <param name="input">The input.</param>
        public static string FromHexStringToBase64String(this string input)
        {
            if (input.Length % 2 != 0) throw new ArgumentOutOfRangeException(nameof(input), "Hex string must have even length.");

            return System.Convert.ToBase64String(input.FromHexStringToHexByteArray());
        }


        /// <summary>
        /// Froms hex string to hex byte array.
        /// </summary>
        /// <param name="inputHex">The input hex.</param>
        public static byte[] FromHexStringToHexByteArray(this string inputHex)
        {
            if (inputHex.Length % 2 != 0) throw new ArgumentOutOfRangeException(nameof(inputHex), "Hex string must have even length.");

            var resultantArray = new byte[inputHex.Length / 2];
            for (var i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = System.Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
            }
            return resultantArray;
        }


        /// <summary>
        /// Generates name prefix.
        /// </summary>
        /// <param name="name">The name.</param>
        public static string GenerateNamePrefix(this string name)
        {
            name = name.Trim();

            if (string.IsNullOrEmpty(name)) return name;

            string prefix = "";

            string[] nameBreakUp = name.Split(' ');

            if (nameBreakUp.Length == 1)
            {
                string firstName = nameBreakUp[0];

                prefix = $"{firstName[0]}{firstName[firstName.Length - 1]}";
            }
            else
            {
                prefix = $"{nameBreakUp[0][0]}{nameBreakUp[nameBreakUp.Length - 1][0]}";
            }

            return prefix;
        }

        /// <summary>
        /// Deserializes from xml.
        /// </summary>
        /// <param name="xml">The xml.</param>
        /// <typeparam name="T"></typeparam>
        public static T DeserializeFromXml<T>(this string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

    }
}


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
            return System.Convert.ToBase64String(input.FromHexStringToHexByteArray());
        }


        /// <summary>
        /// Froms hex string to hex byte array.
        /// </summary>
        /// <param name="inputHex">The input hex.</param>
        public static byte[] FromHexStringToHexByteArray(this string inputHex)
        {
            var resultantArray = new byte[inputHex.Length / 2];
            for (var i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = System.Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
            }
            return resultantArray;
        }
    }
}

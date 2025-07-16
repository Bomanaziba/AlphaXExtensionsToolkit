using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AlphaX.Extensions.HttpContent
{
    public static class HttpContentExtensions
    {

        /// <summary>
        /// Https content to json string.
        /// </summary>
        /// <param name="content">The content.</param>
        public static async Task<string> HttpContentToJsonString(this System.Net.Http.HttpContent content)
        {
            using (var responseStream = await content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(responseStream))
                {
                    using (JsonReader reader = new JsonTextReader(streamReader))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        using (StringWriter textWriter = new StringWriter())
                        {
                            serializer.Serialize(textWriter, reader);
                            return textWriter.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Https content to json string async.
        /// </summary>
        /// <param name="content">The content.</param>
        public static async Task<string> HttpContentToJsonStringAsync(this System.Net.Http.HttpContent content)
        {
            using (var responseStream = await content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(responseStream))
                {
                    using (JsonReader reader = new JsonTextReader(streamReader))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        using (StringWriter textWriter = new StringWriter())
                        {
                            serializer.Serialize(textWriter, reader);
                            return textWriter.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Https content to type async.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <typeparam name="T"></typeparam>
        public static async Task<T> HttpContentToTypeAsync<T>(this System.Net.Http.HttpContent content)
        {
            using (var responseStream = await content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(responseStream))
                {
                    using (JsonReader reader = new JsonTextReader(streamReader))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        return serializer.Deserialize<T>(reader);
                    }
                }
            }
        }

        /// <summary>
        /// Https content to type.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <typeparam name="T"></typeparam>
        public static async Task<T> HttpContentToType<T>(this System.Net.Http.HttpContent content)
        {
            using (var responseStream = await content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(responseStream))
                {
                    using (JsonReader reader = new JsonTextReader(streamReader))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        return serializer.Deserialize<T>(reader);
                    }
                }
            }
        }
    }
}


using Newtonsoft.Json;

namespace AlphaX.Extensions.HttpContent
{
    public static class HttpContentExtensions
    {

        /// <summary>
        /// Https content to json string.
        /// </summary>
        /// <param name="content">The content.</param>
        public static string HttpContentToJsonString(this System.Net.Http.HttpContent content)
        {
            using var responseStream = content.ReadAsStream();
            using var streamReader = new StreamReader(responseStream);
            using JsonReader reader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new();
            using StringWriter textWriter = new();
            serializer.Serialize(textWriter, reader);
            return textWriter.ToString();
        }

        /// <summary>
        /// Https content to json string async.
        /// </summary>
        /// <param name="content">The content.</param>
        public static async Task<string> HttpContentToJsonStringAsync(this System.Net.Http.HttpContent content)
        {
            using var responseStream = await content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(responseStream);
            using JsonReader reader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new();
            using StringWriter textWriter = new();
            serializer.Serialize(textWriter, reader);
            return textWriter.ToString();
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
                        JsonSerializer serializer = new();
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
        public static T HttpContentToType<T>(this System.Net.Http.HttpContent content)
        {
            using var responseStream = content.ReadAsStream();
            using var streamReader = new StreamReader(responseStream);
            using JsonReader reader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new();
            return serializer.Deserialize<T>(reader);
        }
    }
}


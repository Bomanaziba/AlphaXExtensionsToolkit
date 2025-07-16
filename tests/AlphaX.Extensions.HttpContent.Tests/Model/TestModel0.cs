using System.Text;
using Newtonsoft.Json;

namespace AlphaX.Extensions.HttpContent.Tests.Model
{
    public class TestModel0
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static System.Net.Http.HttpContent CreateJsonContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
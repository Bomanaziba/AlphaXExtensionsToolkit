using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace AlphaX.Extensions.Generics.Tests.Model
{
    public class SampleModel0
    {
        public string Name { get; set; }
        public string Age { get; set; }
            
        public static IFormFile CreateCsvFormFile(string content, string fileName = "test.csv")
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(bytes);
            return new FormFile(stream, 0, bytes.Length, "file", fileName);
        }
        }


}
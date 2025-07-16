
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AlphaX.Extensions.HttpContent;
using AlphaX.Extensions.HttpContent.Tests.Model;
using Newtonsoft.Json;
using Xunit;

namespace AlphaX.Extensions.HttpContent.Tests
{
    public class HttpContentExtensionTests
    {

        [Fact]
        public async Task HttpContentToJsonStringAsync_ReturnsJsonString()
        {
            var model = new TestModel0 { Id = 2, Name = "Toolkit" };
            var content = TestModel0.CreateJsonContent(model);

            var result = await content.HttpContentToJsonStringAsync();

            Assert.Contains("\"Id\":2", result);
            Assert.Contains("\"Name\":\"Toolkit\"", result);
        }

        [Fact]
        public async Task HttpContentToTypeAsync_DeserializesToType()
        {
            var model = new TestModel0 { Id = 3, Name = "Extensions" };
            var content = TestModel0.CreateJsonContent(model);

            var result = await content.HttpContentToTypeAsync<TestModel0>();

            Assert.Equal(3, result.Id);
            Assert.Equal("Extensions", result.Name);
        }

        [Fact]
        public async Task HttpContentToType_DeserializesToType()
        {
            var model = new TestModel0 { Id = 4, Name = "HttpContent" };
            var content = TestModel0.CreateJsonContent(model);

            var result = await content.HttpContentToType<TestModel0>();

            Assert.Equal(4, result.Id);
            Assert.Equal("HttpContent", result.Name);
        }
    }
}

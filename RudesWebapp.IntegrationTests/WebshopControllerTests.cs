using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RudesWebapp.IntegrationTests
{
    [Collection("Sequential")]
    public class WebshopControllerTests : IntegrationTest
    {
        public WebshopControllerTests(ITestOutputHelper output) : base(output)
        {
        }


        [Theory]
        [InlineData("/webshop/WebshopStart")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            await LoginAsClientUser();

            // Act
            var response = await TestClient.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
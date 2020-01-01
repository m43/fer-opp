using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RudesWebapp.IntegrationTests
{
    [Collection("Sequential")]
    public class LoginTests : IntegrationTest
    {
        public LoginTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task LoginAsAdminUserTest()
        {
            // Arrange

            // Act
            await LoginAsAdminUser();
            var response = await TestClient.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task LoginAsBoardUserTest()
        {
            // Arrange

            // Act
            await LoginAsBoardUser();
            var response = await TestClient.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task LoginAsClientUserTest()
        {
            // Arrange

            // Act
            await LoginAsClientUser();
            var response = await TestClient.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task LoginAsCoachUserTest()
        {
            // Arrange

            // Act
            await LoginAsCoachUser();
            var response = await TestClient.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task LoginAsUserWithAllRolesTest()
        {
            // Arrange
            
            // Act
            await LoginAsUserWithAllRoles();
            var response = await TestClient.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
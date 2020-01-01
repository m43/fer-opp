using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using RudesWebapp.Models;
using Xunit;
using Xunit.Abstractions;

namespace RudesWebapp.IntegrationTests
{
    [Collection("Sequential")]
    public class HomeControllerTests : IntegrationTest
    {
        public HomeControllerTests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Index")]
        [InlineData("/home/index")]
        [InlineData("/HOME/INDEX")]
        [InlineData("/Home/Media")]
        [InlineData("/Home/About")]
        [InlineData("/Home/Juniors")]
        [InlineData("/Home/Seniors")]
        [InlineData("/Home/YoungCadets")]
        [InlineData("/Home/Cadets")]
        [InlineData("/Home/MiniBasketball")]
        [InlineData("/Home/SportSchools")]
        [InlineData("/Identity/Account/Login")]
        [InlineData("/Identity/Account/Register")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetPosts_ReturnsSomePostsThatWereSeeded()
        {
            // Arrange
            await LoginAsAdminUser();

            // Act
            var response = await TestClient.GetAsync("/home/getposts"); // ApiRoutes.Posts.GetAll

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var posts = await response.Content.ReadAsAsync<List<Post>>();
            posts.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetPost_ReturnsSameAsGetPosts()
        {
            // Arrange
            await LoginAsAdminUser();
            var firstResponse = await TestClient.GetAsync("/home/getposts");
            firstResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var posts = await firstResponse.Content.ReadAsAsync<List<Post>>();
            posts.Should().NotBeEmpty();

            foreach (var post in posts)
            {
                // Act                
                var response = await TestClient.GetAsync("/home/getpost/" + post.Id);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task Post_GetPost_ThenRemoveFetched_ThenGet_DoesntReturnDeleted()
        {
            await LoginAsAdminUser();

            var firstResponse = await TestClient.GetAsync("/home/getposts");
            firstResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var posts = await firstResponse.Content.ReadAsAsync<List<Post>>();
            posts.Should().NotBeEmpty();

            int idToDelete = posts.First().Id;
            var secondResponse = await TestClient.DeleteAsync("/home/removepost/" + idToDelete);
            secondResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var thirdResponse = await TestClient.GetAsync("/home/getpost/" + idToDelete);
            thirdResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            // Arrange
            await LoginAsAdminUser();
            Post postToCreate = new Post
            {
                Title = "Novi post",
                Content =
                    "<h2>What is Lorem Ipsum?</h2> <p><strong>Lorem Ipsum</strong> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMinutes(30),
                Image = null,
                PostType = "Novost"
            };
            var stringPayload = JsonConvert.SerializeObject(postToCreate);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var createPostResponse = await TestClient.PostAsync("/home/addpost", httpContent);
            var createdPost = await createPostResponse.Content.ReadAsAsync<Post>();

            // Act
            var response = await TestClient.GetAsync("/home/getpost/" + createdPost.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Post>();
            returnedPost.Id.Should().Be(createdPost.Id);
            returnedPost.Title.Should().Be(postToCreate.Title);
            returnedPost.Content.Should().Be(postToCreate.Content);
            returnedPost.StartDate.Should().Be(postToCreate.StartDate);
            returnedPost.EndDate.Should().Be(postToCreate.EndDate);
            returnedPost.Image.Should().Be(postToCreate.Image);
            returnedPost.PostType.Should().Be(postToCreate.PostType);
        }
    }
}
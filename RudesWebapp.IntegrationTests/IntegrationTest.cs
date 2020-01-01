using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.Testing;
using RudesWebapp.Data;
using RudesWebapp.Models;
using Xunit;
using Xunit.Abstractions;

namespace RudesWebapp.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        private readonly ITestOutputHelper _output;
        private readonly IServiceProvider _serviceProvider;
        private readonly WebApplicationFactory<Startup> _appFactory;

        protected IntegrationTest(ITestOutputHelper output)
        {
            this._output = output;

            Assert.Null(_serviceProvider);
            Assert.Null(_appFactory);

            _appFactory = new CustomWebApplicationFactory<Startup>();
            TestClient = _appFactory.CreateClient();
            _serviceProvider = _appFactory.Services;
        }

        protected Task LoginAsClientUser()
        {
            return AuthenticateAsync(RudesDatabaseSeeder.DummyUser);
        }

        protected Task LoginAsCoachUser()
        {
            return AuthenticateAsync(RudesDatabaseSeeder.DummyCoachUser);
        }

        protected Task LoginAsBoardUser()
        {
            return AuthenticateAsync(RudesDatabaseSeeder.DummyBoardUser);
        }

        protected Task LoginAsAdminUser()
        {
            return AuthenticateAsync(RudesDatabaseSeeder.DummyAdminUser);
        }

        protected Task LoginAsUserWithAllRoles()
        {
            return AuthenticateAsync(RudesDatabaseSeeder.DummyUserWithAllRoles);
        }


        private async Task AuthenticateAsync(User user)
        {
            TestClient.DefaultRequestHeaders.Accept.Clear();
            TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // send get request to receive a cookie and to find the requestVerificationToken hidden in the form 
            HttpResponseMessage getResponse = await TestClient.GetAsync("/Identity/Account/Login");
            getResponse.EnsureSuccessStatusCode();
            string html = await getResponse.Content.ReadAsStringAsync();
            string requestVerificationToken = ParseRequestVerificationToken(html);
            _output.WriteLine(html);

            //setup login data
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Input.Email", user.Email),
                new KeyValuePair<string, string>("Input.Password", RudesDatabaseSeeder.DUMMY_PASSWORD),
                new KeyValuePair<string, string>("Input.RememberMe", "false"),
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerificationToken)
            });

            //send post request to login
            HttpResponseMessage postResponse = await TestClient.PostAsync("/Identity/Account/Login", formContent);
            postResponse.EnsureSuccessStatusCode();
            html = await postResponse.Content.ReadAsStringAsync();
            _output.WriteLine(html);

            // assert that response html contains a logoutForm (aka. login successful)
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var input = doc.DocumentNode.SelectSingleNode("//*[@id=\"logoutForm\"]");
            Assert.NotNull(input);
        }

        private static string ParseRequestVerificationToken(String html)
        {
            HtmlNode.ElementsFlags.Remove("form");
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var input = doc.DocumentNode.SelectSingleNode("//*[@name='__RequestVerificationToken']");
            return input.Attributes["value"].Value;
        }
    }
}
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;

namespace IntegrationsTests
{
    public class TagControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TagControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task GetPercentEndpoint_WhenTagIsWrong_ShouldFail()
        {
            var client = _factory.CreateClient();

            var t = await client.GetAsync("Tags/percent");

            t.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
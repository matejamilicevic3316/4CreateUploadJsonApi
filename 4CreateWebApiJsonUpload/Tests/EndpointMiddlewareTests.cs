using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace _4CreateWebApiJsonUpload.Tests
{
    public class EndpointMiddlewareTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public EndpointMiddlewareTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Middleware_InvalidJsonParseException()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/testmiddleware/unsuccesfull");
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Middleware_NotFoundException()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/testmiddleware/not-found");
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Middleware_ValidationException()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/testmiddleware/validation");
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }
    }
}

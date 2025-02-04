using CarStoreDatabaseAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Xunit;

namespace TestProject.Tests.Middleware
{
    public class EndpointMiddlewareTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public EndpointMiddlewareTests(WebApplicationFactory<Program> factory)
        {
            factory.WithWebHostBuilder(builder =>
            {
                //builder.ConfigureAppConfiguration((context, config) =>
                //{
                //    var settings = new Dictionary<string, string?>
                //    {
                //        { "LocalConnectionString", "Data Source=DESKTOP-SK56QU7;Initial Catalog=4CreateDatabaseTest;Encrypt=False;Integrated Security=True;" }
                //    };

                //    config.AddInMemoryCollection(initialData: settings);
                //});

                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<MedicineContext>(options =>
                    {
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                    });
                });
            });
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


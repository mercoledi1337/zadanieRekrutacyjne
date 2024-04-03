

using RealWorldApp.Core.Tags;
using RealWorldApp.Infrastructure.DAL.Repositories;
using System.Net.Http.Headers;
using System.Net;

namespace RealWorldApp.Api
{
    public class TagBackgroundService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public TagBackgroundService(IServiceProvider serviceProvider)
        { 
            _serviceProvider = serviceProvider;

        }//zmienić nazwę klasy

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetService<ITagRepository>();
            using HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip;
            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://api.stackexchange.com");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            if (service.GetPopulation() == 0)
            {
                for (int i = 0; i < 13; i++)
                {
                    var response = await client.GetAsync("/2.3/tags?page=" + i + "&pagesize=100&order=desc&sort=popular&site=stackoverflow");
                    Rootobject member;

                    if (response.IsSuccessStatusCode)
                    {
                        member = await response.Content.ReadFromJsonAsync<Rootobject>();

                        foreach (var it in member.items)
                        {
                            Tag tmp = Tag.Create(it.name, it.count);
                            await service.AddTag(tmp);
                        }
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

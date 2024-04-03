using Helper.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net;
using RealWorldApp.Core.Tags;
using System.Text.Json;
using RealWorldApp.Infrastructure.DAL.Repositories;
using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;
using RealWorldApp.Core.Exceptions;
using Microsoft.IdentityModel.Tokens;


namespace RealWorldApp.Api.Controllers
{
    [ApiController]
    [Route("Tags")]
    public class TagsController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ITagRepository _tagRepository;
        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        [SwaggerOperation("1 = By names asc, 2 = By name sec, 3 = By count asc, 4 = By count desc")]
        [HttpGet("tags")]
        public async Task<List<Tag>> Tags(int page  = 1, int pageSize = 10, int sortingType = 1)
        {
            var totalPopulation = _tagRepository.GetPopulation();
            var totalPages = (int)Math.Ceiling((decimal)totalPopulation / pageSize);
            List<Tag> tagPerPage;
      
                switch (sortingType)
                {
                case 2:
                    tagPerPage = await _tagRepository.GetAllTagsDescByNames(pageSize, (page - 1) * pageSize);
                    break;
                case 3:
                    tagPerPage = await _tagRepository.GetAllTagsAscByCount(pageSize, (page - 1) * pageSize);
                    break;
                case 4:
                    tagPerPage = await _tagRepository.GetAllTagsDescByCount(pageSize, (page - 1) * pageSize);
                    break;
                default:
                    tagPerPage = await _tagRepository.GetAllTagsAscByNames(pageSize, (page - 1) * pageSize);
                    break;
            }

            if (tagPerPage.Count == 0)
                throw new PageException();



                return tagPerPage;
        }

        [SwaggerOperation("1 = By names asc, 2 = By name sec, 3 = By count asc, 4 = By count desc")]
        [HttpGet("percent")]
        public async Task<string> Percent(string tag)
        {
            var population = _tagRepository.GetPopulation();
            Tag percent = await _tagRepository.GetTagByName(tag);
            if (percent == null || tag.IsNullOrEmpty())
                throw new WrongTagException();

            int lookingPercent = percent.Count;
            decimal res = (decimal)lookingPercent / (decimal)population * 100;
            return new StringBuilder(tag + " have " + Math.Round(res, 2) + "% in 1000 of downloaded tags").ToString();
        }

        [HttpPut("DownloadTags")]
        public async Task<IActionResult> DownloadTags()
        {
            using HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip;
            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://api.stackexchange.com");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
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
                            await _tagRepository.UpdateTag(tmp);     
                        }
                    }
                }

            return Ok();
        }
    }
}

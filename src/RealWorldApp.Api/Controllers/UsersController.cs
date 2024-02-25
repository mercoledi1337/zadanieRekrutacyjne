using Helper.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace RealWorldApp.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
    }
}

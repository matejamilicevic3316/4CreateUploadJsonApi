using Appplication.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace _4CreateWebApiJsonUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestMiddlewareController : ControllerBase
    {
        [HttpGet("unsuccesfull")]
        public IActionResult UnsuccesfullDeserialization()
        {
            throw new UnsucesfullDeserializationException("Unsucesfull Deserialization.");
        }
        [HttpGet("not-found")]
        public IActionResult NotFoundException()
        {
            throw new NotFoundException("Not found.");
        }
        [HttpGet("validation")]
        public IActionResult Validation()
        {
            throw new ValidationException("Unsucesfull validation.");
        }
    }
}

using Appplication.Commands;
using Microsoft.AspNetCore.Mvc;

namespace _4CreateWebApiJsonUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private IImportJsonCommand _importJsonCommand;
        public ImportController(IImportJsonCommand importJsonCommand)
        {
            _importJsonCommand = importJsonCommand;
        }

        [HttpPost]
        public IActionResult Post([FromForm] IFormFile formFile)
        {
            if (formFile == null)
            {
                return BadRequest();
            }



            return Ok();
        }
    }
}

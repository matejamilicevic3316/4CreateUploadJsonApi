using Appplication.Commands.Import;
using Appplication.Commands.Import.Get;
using Appplication.Import.Requests;
using Appplication.Requests.Import;
using Microsoft.AspNetCore.Mvc;

namespace _4CreateWebApiJsonUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrialsController : ControllerBase
    {
        private IImportJsonCommand _importJsonCommand;
        private IGetTrialCommand _getTrialCommand;
        private ISearchTrialsCommand _searchTrialsCommand;
        public TrialsController(IImportJsonCommand importJsonCommand, IGetTrialCommand getTrialcommand, ISearchTrialsCommand searchTrialsCommand)
        {
            _importJsonCommand = importJsonCommand;
            _getTrialCommand = getTrialcommand;
            _searchTrialsCommand = searchTrialsCommand;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]SearchRequest searchRequest)
        {
            return Ok(_searchTrialsCommand.Execute(searchRequest));
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_getTrialCommand.Execute(id));
        }

        [HttpPost]
        public IActionResult Post([FromForm] FormFileRequest formFile)
        {
            if (formFile == null || formFile.File == null)
            {
                return BadRequest();
            }

            var result = _importJsonCommand.Execute(formFile);

            return Ok();
        }
    }
}

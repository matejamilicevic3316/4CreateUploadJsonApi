using Microsoft.AspNetCore.Http;

namespace Appplication.Import.Requests
{
    public class FormFileRequest
    {
        public required IFormFile File { get; set; }
    }
}

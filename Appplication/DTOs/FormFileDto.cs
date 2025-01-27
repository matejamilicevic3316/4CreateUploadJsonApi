using Microsoft.AspNetCore.Http;

namespace Appplication.DTOs
{
    public class FormFileDto
    {
        public required IFormFile File { get; set; }
    }
}

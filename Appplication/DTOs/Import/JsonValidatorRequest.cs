using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appplication.DTOs.Import
{
    public class JsonValidatorRequest
    {
        public required string parsedJsonData { get; set; }
        public required string schemaUrl { get; set; }
    }
}

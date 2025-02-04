using Appplication.DTOs.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appplication.Commands.Import
{
    public interface IJSonValidator
    {
        IEnumerable<string>? Execute(JsonValidatorRequest req);
    }
}

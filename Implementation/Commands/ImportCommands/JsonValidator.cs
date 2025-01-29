using Appplication.Commands.Import;
using Appplication.DTOs.Import;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Implementation.Commands.ImportCommands
{
    public class JsonValidator : IJSonValidator
    {
        public bool Execute(JsonValidatorRequest req)
        {
            string schemaData = File.ReadAllText(req.schemaUrl);

            JSchema schema = JSchema.Parse(schemaData);
            JObject json = JObject.Parse(req.parsedJsonData);

            if (json.IsValid(schema, out IList<string> errors))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

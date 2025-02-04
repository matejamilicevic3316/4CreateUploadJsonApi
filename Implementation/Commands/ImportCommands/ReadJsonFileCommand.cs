using Appplication.Commands.Import;
using Appplication.DTOs.Import;
using Appplication.DTOs.Import.Post;
using Appplication.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Implementation.Commands.ImportCommands
{
    public class ReadJsonFileCommand : IReadJsonFileCommand<TrialDto>
    {
        private IJSonValidator _validator;
        public ReadJsonFileCommand(IJSonValidator validator)
        {
            _validator = validator;
        }

        public TrialDto Execute(IFormFile req)
        {
            TrialDto trialDto;

            using (var memoryStream = req.OpenReadStream())
            {
                if (memoryStream == null)
                {
                    throw new FileNotFoundException();
                }
                var jsonText = new StreamReader(memoryStream).ReadToEnd();

                var jsonErrrList = new List<string>();
                var jsonErrors = _validator.Execute(new JsonValidatorRequest { parsedJsonData = jsonText, schemaUrl = "Embedded/trial-json-schema.json" });

                if (jsonErrors?.Any() ?? false)
                {
                    throw new FluentValidation.ValidationException(jsonErrors.Select(x => new FluentValidation.Results.ValidationFailure { ErrorMessage = x, PropertyName = "JsonField"}));
                }

                trialDto = JsonSerializer.Deserialize<TrialDto>(jsonText)!;
            }

            if (trialDto == null)
            {
                throw new UnsucesfullDeserializationException("Unsuccesful deserialization of trial : " + DateTime.Now);
            }

            return trialDto;
        }
    }
}

﻿using Appplication.Commands.Import;
using Appplication.DTOs.Import;
using Implementation.Commands.ImportCommands;
using Newtonsoft.Json;
using Xunit;

namespace TestProject.Tests.Import
{
    public class JSonValidatorTests
    {
        [Fact]
        public void Invalid_Schema()
        {
            var jsonText = File.ReadAllText("Embedded\\test\\invalid-schema.json");
            IJSonValidator jsonValidator = new JsonValidator();

            var jsonValidationErrors = jsonValidator.Execute(new JsonValidatorRequest { parsedJsonData = jsonText, schemaUrl = "Embedded\\test\\trial-json-schema.json" });
            
            Assert.True(jsonValidationErrors?.Any());
        }
        [Fact]
        public void Valid_Schema()
        {
            var jsonText = File.ReadAllText("Embedded\\test\\valid-schema.json");
            IJSonValidator jsonValidator = new JsonValidator();

            var jsonValidationErrors = jsonValidator.Execute(new JsonValidatorRequest { parsedJsonData = jsonText, schemaUrl = "Embedded\\test\\trial-json-schema.json" });

            Assert.Null(jsonValidationErrors);
        }
    }
}

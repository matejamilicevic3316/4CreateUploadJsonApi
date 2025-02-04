using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Implementation.Validators
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(x => x.Length).LessThan(4000000)
                .WithMessage("Length should not be longer than 4mb");
            RuleFor(x => x.FileName).Matches(".*\\.json$")
                .WithMessage("File must be of json type");
        }
    }
}

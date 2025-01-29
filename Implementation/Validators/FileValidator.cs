using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Implementation.Validators
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(x => x.Length).LessThan(4000000);
            RuleFor(x => x.FileName).Matches(".*\\.json$");
        }
    }
}

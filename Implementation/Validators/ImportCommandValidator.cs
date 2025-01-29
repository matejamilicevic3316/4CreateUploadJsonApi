using Appplication.DTOs.Import.Post;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class ImportCommandValidator : AbstractValidator<TrialDto>
    {
        public ImportCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}

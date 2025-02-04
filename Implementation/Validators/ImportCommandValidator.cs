using Appplication.DTOs.Import.Post;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class ImportCommandValidator : AbstractValidator<TrialDto>
    {
        public ImportCommandValidator()
        {
            RuleFor(x => x.TrialId).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate)
                .Must((x, y) => y != null ? x.StartDate < y : true);
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}

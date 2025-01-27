using Appplication.DTOs;

namespace Appplication.Commands
{
    public interface IImportJsonCommand : BaseCommand<FormFileDto, TrialReadDto>
    {
    }
}

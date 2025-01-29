using Appplication.DTOs.Import.Get;
using Appplication.Import.Requests;

namespace Appplication.Commands.Import
{
    public interface IImportJsonCommand : IBaseCommand<FormFileRequest, TrialReadDto>
    {
    }
}

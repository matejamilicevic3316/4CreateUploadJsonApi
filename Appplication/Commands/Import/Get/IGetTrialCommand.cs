using Appplication.DTOs.Import.Get;
using Appplication.DTOs;
using Appplication.Requests.Import;

namespace Appplication.Commands.Import.Get
{
    public interface IGetTrialCommand : IBaseCommand<string, TrialReadDto>
    {
    }
}

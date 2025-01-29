using Appplication.DTOs;
using Appplication.DTOs.Import.Get;
using Appplication.Requests.Import;

namespace Appplication.Commands.Import.Get
{
    public interface ISearchTrialsCommand : IBaseCommand<SearchRequest, PagedResponse<TrialReadDto>>
    {
    }
}

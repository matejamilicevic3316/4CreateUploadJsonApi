using Appplication.Commands.Import.Get;
using Appplication.DTOs;
using Appplication.DTOs.Import.Get;
using Appplication.DTOs.Import.Post;
using Appplication.Requests.Import;
using AutoMapper;
using CarStoreDatabaseAccess;
using Domain;

namespace Implementation.Commands.GetCommands
{
    public class SearchTrialsCommand : BaseCommand<SearchRequest, PagedResponse<TrialReadDto>>, ISearchTrialsCommand
    {
        public SearchTrialsCommand(MedicineContext medicineContext, IMapper mapper) : base(medicineContext, mapper)
        {
        }

        public override PagedResponse<TrialReadDto> Execute(SearchRequest req)
        {
            List<Trial> trials = new List<Trial>();
            
            if (req.Keyword != null)
            {
                trials = _medicineContext.Trials.Where(x =>
                    x.Id.Contains(req.Keyword) ||
                    x.Title.Contains(req.Keyword)).ToList();
            }
            else
            {
                trials = _medicineContext.Trials.ToList();
            }

            var trialDtos = _mapper.Map<ICollection<TrialReadDto>>(trials);

            return new PagedResponse<TrialReadDto>
            {
                Dtos = trialDtos,
                Page = req.Page,
                PerPage = req.PerPage,
                TotalPages = (int)Math.Ceiling((decimal)trialDtos.Count / req.PerPage)
            };
        }
    }
}

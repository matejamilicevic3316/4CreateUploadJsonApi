using Appplication;
using Appplication.Commands.Import.Get;
using Appplication.DTOs;
using Appplication.DTOs.Import.Get;
using Appplication.Exceptions;
using Appplication.Requests.Import;
using AutoMapper;
using _4CreateWebApiJsonUpload;

namespace Implementation.Commands.GetCommands
{
    public class GetTrialCommand : BaseCommand<string, TrialReadDto>, IGetTrialCommand
    {
        public GetTrialCommand(MedicineContext medicineContext, IMapper mapper) : base(medicineContext, mapper)
        {
        }
        public override TrialReadDto Execute(string req)
        {
            var trial = _medicineContext.Trials.FirstOrDefault(x => x.TrialId == req);
            
            if (trial == null)
            {
                throw new NotFoundException();
            }
            
            var trialDto = _mapper.Map<TrialReadDto>(trial);

            return trialDto;
        }

    }
}

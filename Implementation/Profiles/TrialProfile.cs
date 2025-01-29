using Appplication.DTOs.Import.Get;
using Appplication.DTOs.Import.Post;
using AutoMapper;
using Domain;

namespace Implementation.Profiles
{
    public class TrialProfile : Profile
    {
        public TrialProfile()
        {
            CreateMap<Trial, TrialReadDto>()
                .ForMember(x => x.Status, x => x.MapFrom(y => y.Status.ToString()));

            CreateMap<TrialDto, Trial>()
                .ForMember(x => x.EndDate, x => x.MapFrom(y => y.EndDate != null ? y.EndDate : y.StartDate.AddMonths(1)))
                .ForMember(x => x.Status, x => x.MapFrom(y => Enum.GetValues<Status>().FirstOrDefault(x => x.ToString() == y.Status)));
        }
    }
}

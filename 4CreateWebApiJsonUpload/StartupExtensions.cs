using AutoMapper;
using BusinessLogic.Validators;
using FluentValidation;
using Implementation.Profiles;

namespace CarStore.Api
{
    public static class StartupExtensions
    {
        public static void SetupMapper(this IServiceCollection services)
        {
            var assembly = typeof(TrialProfile).Assembly;
            var profiles = assembly.GetTypes().Where(x => x.IsClass && x.IsSubclassOf(typeof(Profile)));
            var mapper = new MapperConfiguration(x => { 
                foreach(var profile in profiles)
                {
                    var parsedProfile = Activator.CreateInstance(profile) as Profile;
                    x.AddProfile(parsedProfile);
                }
            });
            var IMapper = mapper.CreateMapper();
            services.AddTransient(x => IMapper);
        }

        public static void AddValidators(this IServiceCollection services)
        {
            var assembly = typeof(ImportCommandValidator).Assembly;
            var types = assembly.DefinedTypes.Where(x => x.GetInterfaces()
                                        .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>))).ToList();
            foreach(var type in types)
            {
                foreach(var validator in type.ImplementedInterfaces)
                {
                    services.AddTransient(validator, type);
                }
            }
        }
    }
}

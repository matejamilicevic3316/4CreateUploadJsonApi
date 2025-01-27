using Appplication.Commands;
using Appplication.DTOs;
using CarStoreDatabaseAccess;
using System.Reflection;

namespace Implementation.Commands
{
    public class ImportJsonCommand : IImportJsonCommand
    {
        private MedicineContext _medicineContext;
        public ImportJsonCommand(MedicineContext medicineContext)
        {
            _medicineContext = medicineContext;
        }

        public TrialReadDto Execute(FormFileDto req)
        {
            using (var memoryStream = new MemoryStream())
            {
                var assemblyStream = Assembly.GetExecutingAssembly();
            }

            return new TrialReadDto();
        }
    }
}

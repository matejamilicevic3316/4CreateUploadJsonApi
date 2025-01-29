using Appplication.Commands.Import;
using Appplication.DTOs.Import.Get;
using Appplication.DTOs.Import.Post;
using Appplication.Exceptions;
using Appplication.Import.Requests;
using AutoMapper;
using CarStoreDatabaseAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using ValidationException = FluentValidation.ValidationException;

namespace Implementation.Commands.ImportCommands
{
    public class ImportJsonCommand : BaseCommand<FormFileRequest,TrialReadDto>,IImportJsonCommand
    {
        private IReadJsonFileCommand<TrialDto> _readJsonCommand;
        private IValidator<TrialDto> _trialValidator;
        private IValidator<IFormFile> _fileValidator;

        public ImportJsonCommand(MedicineContext medicineContext, IMapper mapper, IValidator<TrialDto> validator, IValidator<IFormFile> fileValidator, IReadJsonFileCommand<TrialDto> readJsonCommand) : base(medicineContext, mapper)
        {
            _trialValidator = validator;
            _fileValidator = fileValidator;
            _readJsonCommand = readJsonCommand;
        }

        public override TrialReadDto Execute(FormFileRequest req)
        {
            var fileValidationResult = _fileValidator.Validate(req.File);

            if (fileValidationResult != null && !fileValidationResult.IsValid)
            {
                throw new ValidationException(fileValidationResult.Errors);
            }

            TrialDto trialDto = _readJsonCommand.Execute(req.File);

            var validationResult = _trialValidator.Validate(trialDto);

            if (validationResult != null && !validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var trial = _medicineContext.Trials.FirstOrDefault(x => x.Id == trialDto.Id);

            if (trial != null)
            {
                trial = _mapper.Map<Trial>(trialDto);
            }
            else
            {
                trial = _mapper.Map<Trial>(trialDto);
                _medicineContext.Trials.Add(trial);
            }

            _medicineContext.SaveChanges();

            return _mapper.Map<TrialReadDto>(trial);
        }
    }
}

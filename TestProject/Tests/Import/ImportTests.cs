using Appplication.Commands.Import;
using Appplication.DTOs.Import.Post;
using Appplication.Exceptions;
using Appplication.Import.Requests;
using AutoMapper;
using CarStoreDatabaseAccess;
using Domain;
using FluentValidation;
using FluentValidation.Results;
using Implementation.Commands.ImportCommands;
using Implementation.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace TestProject.Tests.Import
{
    public class ImportTests
    {
        private MedicineContext? _context;
        private MedicineContext Context
        {
            get
            {
                if (_context != null)
                {
                    return _context;
                }
                else
                {
                    var context = SetDefaultContext();

                    return context;
                }
            }
        }

        private MedicineContext SetDefaultContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MedicineContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MedicineContext(optionsBuilder.Options);

            for (int i = 0; i < 10; i++)
            {
                context.Trials.Add(new Trial
                {
                    TrialId = "Id" + (i + 1),
                    StartDate = DateTime.Now.AddMonths(i),
                    EndDate = DateTime.Now.AddMonths(1 + i),
                    Title = "Title" + (i + 1),
                    Participants = i + 1,
                    Status = Status.Ongoing
                });
            }

            context.SaveChanges();

            _context = context;

            return context;
        }

        IMapper _mapper = new MapperConfiguration(x => x.AddProfile(new TrialProfile())).CreateMapper();
        Mock<IValidator<TrialDto>> _trialValidator = new Mock<IValidator<TrialDto>>();
        Mock<IValidator<IFormFile>> _fileValidator = new Mock<IValidator<IFormFile>>();
        Mock<IReadJsonFileCommand<TrialDto>> _readJsonCommand = new Mock<IReadJsonFileCommand<TrialDto>>();

        [Fact]
        public void Update_Happy_Case()
        {
            _trialValidator.Setup(x => x.Validate(It.IsAny<TrialDto>())).Returns(new ValidationResult());
            _fileValidator.Setup(x => x.Validate(It.IsAny<IFormFile>())).Returns(new ValidationResult());
            _readJsonCommand.Setup(x => x.Execute(It.IsAny<IFormFile>())).Returns(new TrialDto { TrialId = "Id2", Status = Status.Completed.ToString(), Title = "Title1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), Participants = 5 });

            var databaseExistingRecord = Context.Trials.FirstOrDefault(x => x.TrialId == "Id2");

            if (databaseExistingRecord == null)
            {
                Assert.Fail();
            }

            Assert.Equal("Title2", databaseExistingRecord.Title);
            Assert.Equal(Status.Ongoing, databaseExistingRecord.Status);

            IImportJsonCommand importJsonCommand = new ImportJsonCommand(Context, _mapper, _trialValidator.Object, _fileValidator.Object, _readJsonCommand.Object);

            Mock<IFormFile> formFile = new Mock<IFormFile>();

            var result = importJsonCommand.Execute(new FormFileRequest
            {
                File = formFile.Object
            });

            Assert.Equal("Title1", result.Title);
            Assert.Equal(Status.Completed.ToString(), result.Status);

            SetDefaultContext();
        }

        [Fact]
        public void Insert_Happy_Case()
        {
            _trialValidator.Setup(x => x.Validate(It.IsAny<TrialDto>())).Returns(new ValidationResult());
            _fileValidator.Setup(x => x.Validate(It.IsAny<IFormFile>())).Returns(new ValidationResult());
            _readJsonCommand.Setup(x => x.Execute(It.IsAny<IFormFile>())).Returns(new TrialDto { TrialId = "Id11", Status = Status.Completed.ToString(), Title = "Title10", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), Participants = 5 });

            var databaseExistingRecord = Context.Trials.FirstOrDefault(x => x.TrialId == "Id11");

            Assert.Null(databaseExistingRecord);

            IImportJsonCommand importJsonCommand = new ImportJsonCommand(Context, _mapper, _trialValidator.Object, _fileValidator.Object, _readJsonCommand.Object);

            Mock<IFormFile> formFile = new Mock<IFormFile>();

            var result = importJsonCommand.Execute(new FormFileRequest
            {
                File = formFile.Object
            });

            Assert.Equal("Title10", result.Title);
            Assert.Equal(Status.Completed.ToString(), result.Status);
            Assert.Equal("Id11", result.TrialId);
            Assert.Equal(5, result.Participants);

            SetDefaultContext();
        }

        [Fact]
        public void Insert_FileParseFails()
        {
            _trialValidator.Setup(x => x.Validate(It.IsAny<TrialDto>())).Returns(new ValidationResult());
            _fileValidator.Setup(x => x.Validate(It.IsAny<IFormFile>())).Returns(new ValidationResult());
            _readJsonCommand.Setup(x => x.Execute(It.IsAny<IFormFile>())).Throws(new UnsucesfullDeserializationException());

            var databaseExistingRecord = Context.Trials.FirstOrDefault(x => x.TrialId == "Id11");

            Assert.Null(databaseExistingRecord);

            IImportJsonCommand importJsonCommand = new ImportJsonCommand(Context, _mapper, _trialValidator.Object, _fileValidator.Object, _readJsonCommand.Object);

            Mock<IFormFile> formFile = new Mock<IFormFile>();

            try
            {
                var result = importJsonCommand.Execute(new FormFileRequest
                {
                    File = formFile.Object
                });

                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(UnsucesfullDeserializationException), ex.GetType());
            }
        }

        [Fact]
        public void Insert_FileValidationFails()
        {
            _trialValidator.Setup(x => x.Validate(It.IsAny<TrialDto>())).Returns(new ValidationResult());
            _fileValidator.Setup(x => x.Validate(It.IsAny<IFormFile>())).Throws(new ValidationException(""));
            _readJsonCommand.Setup(x => x.Execute(It.IsAny<IFormFile>())).Returns(new TrialDto { TrialId = "Id11", Status = Status.Completed.ToString(), Title = "Title10", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), Participants = 5 });

            var databaseExistingRecord = Context.Trials.FirstOrDefault(x => x.TrialId == "Id11");

            Assert.Null(databaseExistingRecord);

            IImportJsonCommand importJsonCommand = new ImportJsonCommand(Context, _mapper, _trialValidator.Object, _fileValidator.Object, _readJsonCommand.Object);

            Mock<IFormFile> formFile = new Mock<IFormFile>();

            try
            {
                var result = importJsonCommand.Execute(new FormFileRequest
                {
                    File = formFile.Object
                });

                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ValidationException), ex.GetType());
            }
        }

        [Fact]
        public void Insert_TrialValidatorFails()
        {
            _trialValidator.Setup(x => x.Validate(It.IsAny<TrialDto>())).Throws(new ValidationException(""));
            _fileValidator.Setup(x => x.Validate(It.IsAny<IFormFile>())).Returns(new ValidationResult());
            _readJsonCommand.Setup(x => x.Execute(It.IsAny<IFormFile>())).Returns(new TrialDto { TrialId = "Id11", Status = Status.Completed.ToString(), Title = "Title10", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), Participants = 5 });

            var databaseExistingRecord = Context.Trials.FirstOrDefault(x => x.TrialId == "Id11");

            Assert.Null(databaseExistingRecord);

            IImportJsonCommand importJsonCommand = new ImportJsonCommand(Context, _mapper, _trialValidator.Object, _fileValidator.Object, _readJsonCommand.Object);

            Mock<IFormFile> formFile = new Mock<IFormFile>();

            try
            {
                var result = importJsonCommand.Execute(new FormFileRequest
                {
                    File = formFile.Object
                });

                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ValidationException), ex.GetType());
            }
        }
    }
}

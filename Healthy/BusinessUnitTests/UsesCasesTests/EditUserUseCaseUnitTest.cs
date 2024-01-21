using Healthy.Business.Commands;
using Healthy.Business.Specifications.Definitions;
using Healthy.Business.UseCases.Implementations;
using Healthy.Domain.Constants;
using Healthy.Domain.Entities;
using Healthy.Infraestructure;
using Healthy.UnitTest.Commond;
using Moq;

namespace Healthy.Business.UnitTests.UsesCasesTests
{
    public class EditUserUseCaseUnitTest
    {
        private readonly HealthyDbContext _db;
        private Mock<IUserSpecification> _userSpecificationMock;
        private EditUserUseCase _editUserUseCase;
        private EditUserCommand _editUserCommand;
        private IEnumerable<string> _errors = new List<string>();
        public EditUserUseCaseUnitTest()
        {
            _db = new ContextInMemory().GetContext();
            _userSpecificationMock = new Mock<IUserSpecification>();
            _editUserCommand = new EditUserCommand()
            {
                FirstName = "Felipe",
                LastFirstName = "Sotelo",
                BirthDayDate = new DateTime(1985, 12, 4),
                Email = "felipe.sotelo@live.com",
                NickName = "ingphillip",
                Password = "Felipe.8512",
                PhoneNumber = "1234567890",
                PhoneCodeArea = "57",
            };
            _userSpecificationMock.Setup(x => x.Valid(_editUserCommand)).Returns(() => _errors);
            _editUserUseCase = new EditUserUseCase(_userSpecificationMock.Object, _db);
          
            _db.Users.Add(_editUserCommand as User);
            _db.SaveChanges();

        }
        [Fact]
        public async void EditUserUseCaseExecute_WhenTrySaveUserIntoDatabase_ReturnEmptyListOfErrorsAndEditUser()
        {
            _editUserCommand.Email = "felipe.sotelo@globant.com";
            _editUserCommand.FirstName = "felipe";
            _editUserCommand.SecondName = "armando";
            _editUserCommand.LastFirstName = "sotelo";
            _editUserCommand.LastSecondName = "sotelo";
            _editUserCommand.BirthDayDate = new DateTime(1985, 12, 3);
            _editUserCommand.NickName = "fsotelos";

            var results = await _editUserUseCase.Execute(_editUserCommand);

            var resultsEditedUser = _db.Users.FirstOrDefault(x => x.Id == _editUserCommand.Id);

            Assert.Equal(_editUserCommand.Id, resultsEditedUser.Id);
            Assert.Equal(_editUserCommand.Email, resultsEditedUser.Email);
            Assert.Equal(_editUserCommand.FirstName, resultsEditedUser.FirstName);
            Assert.Equal(_editUserCommand.SecondName, resultsEditedUser.SecondName);
            Assert.Equal(_editUserCommand.LastFirstName, resultsEditedUser.LastFirstName);
            Assert.Equal(_editUserCommand.LastSecondName, resultsEditedUser.LastSecondName);
            Assert.Equal(_editUserCommand.BirthDayDate, resultsEditedUser.BirthDayDate);
            Assert.Equal(_editUserCommand.NickName, resultsEditedUser.NickName);
            Assert.True(resultsEditedUser is not null && results.Errors.Count() == 0);
        }

        [Fact]
        public async void EditUserUseCaseExecute_WhenTryEditInvalidUserIntoDatabase_ReturnErrorsAndNotEditUser()
        {
            _editUserCommand.Email = "felipe.sotelolive.com";
            
            _errors = new List<string>()
            {
                Constants.UserSpecificationEmailError,
            };
            var results = await _editUserUseCase.Execute(_editUserCommand);

            var resultUserEdited = _db.Users.Any(x => x.Email == "felipe.sotelolive.com" );

            Assert.True(!resultUserEdited && results.Errors.Count() > 0);
        }
    }
}

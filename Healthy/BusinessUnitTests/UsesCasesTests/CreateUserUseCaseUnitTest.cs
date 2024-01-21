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
    public class CreateUserUseCaseUnitTest
    {
        private readonly HealthyDbContext _db;
        private Mock<IUserSpecification> _userSpecificationMock;
        private CreateUserUseCase _createUserUseCase;
        private RegisterUserCommand _userRegisterCommand;
        private IEnumerable<string> _errors = new List<string>();
        public CreateUserUseCaseUnitTest()
        {
            _db = new ContextInMemory().GetContext();
            _userSpecificationMock = new Mock<IUserSpecification>();
            _userRegisterCommand = new RegisterUserCommand()
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
            _userSpecificationMock.Setup(x => x.Valid(_userRegisterCommand)).Returns(() => _errors);
            _createUserUseCase = new CreateUserUseCase(_userSpecificationMock.Object, _db);
           
        }
        [Fact]
        public async void CreateUserUseCaseExecute_WhenTrySaveUserIntoDatabase_ReturnEmptyListOfErrorsAndSaveUser()
        {
            var results = await _createUserUseCase.Execute(_userRegisterCommand);

            var resultUserInserted = _db.Users.Any(x => x.Email == "felipe.sotelo@live.com");

            Assert.True(resultUserInserted && results.Errors.Count() == 0);
        }

        [Fact]
        public async void CreateUserUseCaseExecute_WhenTrySaveInvalidUserIntoDatabase_ReturnListOfErrorsAndNotInsertUser()
        {
            _userRegisterCommand.Email = "felipe.sotelolive.com";
            _userRegisterCommand.FirstName = string.Empty;
            _errors = new List<string>()
            {
                Constants.UserSpecificationEmailError,
                Constants.UserSpecificationFirstNameNullEmptyOrWithSpace
            };
            var results = await _createUserUseCase.Execute(_userRegisterCommand);

            var resultUserInserted = _db.Users.Any(x => x.Email == "felipe.sotelolive.com");

            Assert.True(!resultUserInserted && results.Errors.Count() > 0);
        }


    }
}

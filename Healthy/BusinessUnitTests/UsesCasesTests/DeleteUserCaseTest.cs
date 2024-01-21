using Healthy.Business.Commands;
using Healthy.Business.UseCases.Definitions;
using Healthy.Business.UseCases.Implementations;
using Healthy.Domain.Constants;
using Healthy.Domain.Entities;
using Healthy.Infraestructure;
using Healthy.UnitTest.Commond;

namespace Healthy.Business.UnitTests.UsesCasesTests
{
    public class DeleteUserCaseTest
    {
        public IDeleteUserUseCase _deleteUserCase;
        HealthyDbContext _db;
        User _user;
        public DeleteUserCaseTest() {
            _db = new ContextInMemory().GetContext();
            _deleteUserCase = new DeleteUserUseCase(_db);
            _user = new User()
            {
                FirstName = "Felipe",
                LastFirstName = "Sotelo",
                BirthDayDate = new DateTime(1985, 12, 4),
                Email = "felipe1.sotelo@live.com",
                NickName = "ing1phillip",
                Password = "Felipe.8512",
                PhoneNumber = "1234567890",
                PhoneCodeArea = "57",
            };
            _db.Users.Add(_user); 
            _db.SaveChanges();
        }

        [Fact]
        public async void DeleteUserUseCaseExecute_WhenSaveDeleteAction_ReturnEmptyListOfErrorsAndChanges()
        {
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand();
           
            deleteUserCommand.Id = _db.Users.FirstOrDefault(x => x.Email == "felipe1.sotelo@live.com").Id;
            
            var results = await _deleteUserCase.Execute(deleteUserCommand);

            var resultUserDeleted = !_db.Users.Any(x => x.Email == "felipe1.sotelo@live.com");

            Assert.True(resultUserDeleted && results.Errors.Count() == 0);
        }


        [Fact]
        public async void DeleteUserUseCaseExecute_WhenSaveDeleteAction_ReturnAnyErrorsAndUserNotDelete()
        {
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand();

            deleteUserCommand.Id = 1000;

            var results = await _deleteUserCase.Execute(deleteUserCommand);

            var resultUserExist = _db.Users.Any(x => x.Email == "felipe1.sotelo@live.com");

            Assert.True(resultUserExist && results.Errors.Count() > 0);
            Assert.Contains(Constants.UserNotExist, results.Errors);
        }
    }
}

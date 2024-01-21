using Healthy.Business.Specifications.Implementations;
using Healthy.Domain.Constants;
using Healthy.Domain.Entities;
using Healthy.Infraestructure;
using Healthy.UnitTest.Commond;

namespace Healthy.Business.UnitTests.SpecificationsTests
{
    public class UserEspecificationUnitTest
    {
        private readonly HealthyDbContext _db;
        UserSpecification _createUserSpecification;
        User _user;
        public UserEspecificationUnitTest()
        {
            _db = new ContextInMemory().GetContext();
            _createUserSpecification = new UserSpecification(_db);
            _user = new()
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
        }

        [Fact]
        public void ValidUser_WhenCallAndUserIsNull_ReturnErrorForNullUser()
        {
            _user = null;

            var resultValidation = _createUserSpecification.Valid(_user);

            Assert.Contains(Constants.UserSpecificationUserNullError, resultValidation);
        }

        [Fact]
        public void ValidUser_WhenCall_ReturnsErrorOfInvalidEmail()
        {
            _user.Email = "cadenaerrada";

            var resultValidation = _createUserSpecification.Valid(_user);

            Assert.Contains(Constants.UserSpecificationEmailError, resultValidation);
        }

        [Fact]
        public void ValidUser_WhenCall_ReturnsErrorOfInvalidBirthDay()
        {
            _user.BirthDayDate = DateTime.Now.AddYears(-(Constants.UserSpecificationMinYearsOld - 1));

            var resultValidation = _createUserSpecification.Valid(_user);

            Assert.Contains(Constants.UserSpecificationMinBirthDay, resultValidation);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidUser_WhenCall_ReturnsErrorOfNullEmptyOrWhiteSpaceFirstName(string firstName)
        {
            _user.FirstName = firstName;

            var resultValidation = _createUserSpecification.Valid(_user);

            Assert.Contains(Constants.UserSpecificationFirstNameNullEmptyOrWithSpace, resultValidation);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidUser_WhenCall_ReturnsErrorOfNullEmptyOrWhiteSpaceLastFirstName(string lastFirstName)
        {
            _user.LastFirstName = lastFirstName;

            var resultValidation = _createUserSpecification.Valid(_user);

            Assert.Contains(Constants.UserSpecificationLastFirstNameNullEmptyOrWithSpace, resultValidation);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidUser_WhenCall_ReturnsErrorOfNullEmptyOrWhiteSpaceNickName(string nickName)
        {
            _user.NickName = nickName;

            var resultValidation = _createUserSpecification.Valid(_user);

            Assert.Contains(Constants.UserSpecificationNickNameNullEmptyOrWithSpace, resultValidation);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidUser_WhenCall_ReturnsErrorOfNullEmptyOrWhiteSpacePhoneNumber(string nickName)
        {
            _user.NickName = nickName;

            var resultValidation = _createUserSpecification.Valid(_user);

            Assert.Contains(Constants.UserSpecificationNickNameNullEmptyOrWithSpace, resultValidation);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("felipesotelo")]
        [InlineData("SOTELOFELIPE")]
        [InlineData("SOTELOFELIPE1")]
        [InlineData("SOTELOFELIPE$")]
        public void ValidUser_WhenCall_ReturnsErrorOfInvalidPasswordEmptyOrWhiteSpace(string password)
        {
            _user.Password = password;

            var resultValidation = _createUserSpecification.Valid(_user);

            Assert.Contains(Constants.UserSpecificationInvalidPassword, resultValidation);
        }

        [Fact]
        public void ValidUser_WhenTrySaveDuplicateUserEmailIntoDatabase_ReturnErrorOfExistigEmailAndNoInsert()
        {
            _db.Users.Add(_user);
            _user.Email = "aguila@uno.com";
            _db.SaveChanges();
            _user.Id = 0;

            var results = _createUserSpecification.Valid(_user);

            var resultUserInserted = _db.Users.Count(x => x.Email == "aguila@uno.com");

            Assert.Contains(Constants.UserSpecificationDuplicatedEmail, results);
            Assert.True(resultUserInserted == 1 && results.Count() > 0);
        }

        [Fact]
        public async void ValidUser_WhenTrySaveDuplicateUserNickNameIntoDatabase_ReturnErrorOfExistigNickNameAndNoInsert()
        {
            _db.Users.Add(_user);
            _user.Email = "f@f.co";
            _user.NickName = "prueba2";
            _db.SaveChanges();
            _user.Email = "f@felipe.co";
            _user.Id = 0;

            var results = _createUserSpecification.Valid(_user);

            var resultUserInserted = _db.Users.Count(x => x.NickName == _user.NickName);

            Assert.Contains(Constants.UserSpecificationDuplicatedNickName, results);
            Assert.True(resultUserInserted == 1 && results.Count() > 0);
        }

        [Fact]
        public async void EditUser_WhenTrySaveAndDetectNoExistUser_ReturnErrorOfUserNotExist()
        {
            _db.Users.Add(_user);
            await _db.SaveChangesAsync();
            _user.Email = "felipe.sotelo@globant.com";
            _user.NickName = "prueba1";
            _user.Id = 10;

            var results = _createUserSpecification.Valid(_user).ToList();

            var resultUserInserted = _db.Users.Count(x => x.NickName == _user.NickName);

            Assert.Contains(Constants.UserSpecificationNotExistEditingUser, results);
            Assert.True(resultUserInserted == 0 && results.Count() > 0);
        }
    }
}
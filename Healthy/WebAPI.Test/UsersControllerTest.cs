using Healthy.Business.Commands;
using Healthy.Business.Services.Definitions;
using Healthy.Domain.Constants;
using Healthy.Domain.Entities;
using Healthy.Domain.Responses;
using Healthy.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebAPI.Test
{
    public class UsersControllerTest
    {
        UsersController _usersController;
        Mock<IServiceUsers> _serviceUserMock;
        
        //User _user;
        public UsersControllerTest()
        {
            _serviceUserMock = new Mock<IServiceUsers>();
            _usersController = new UsersController(_serviceUserMock.Object);

        }
        [Fact]
        public async void CreateNewUser_VerifyIfUserWasCreatedSucessful_ReturnsOkObjectResult()
        {
            Response<RegisterUserCommand> _response = new Response<RegisterUserCommand>();
            _serviceUserMock
            .Setup(x => x.CreateUser(It.IsAny<RegisterUserCommand>()))
            .Returns(() => Task.FromResult(_response));

            var resultCreateUser = await _usersController.CreateUser(It.IsAny<RegisterUserCommand>());

            Assert.IsType<OkObjectResult>(resultCreateUser);

            Assert.Equal(Constants.UserCreatedSucessful, ((Response<RegisterUserCommand>)((OkObjectResult)resultCreateUser).Value).Message);
        }

        [Fact]
        public async Task CreateNewUser_InvalidUserData_ReturnsBadRequestWithErrors()
        {
            Response<RegisterUserCommand> _response = new Response<RegisterUserCommand>();
            _response.Errors = new List<string>()
            {
                Constants.UserSpecificationEmailError,
                Constants.UserSpecificationFirstNameNullEmptyOrWithSpace
            };

            _serviceUserMock
            .Setup(x => x.CreateUser(It.IsAny<RegisterUserCommand>()))
            .Returns(() => Task.FromResult(_response));
            // Arrange


            // Act
            var resultCreateUser = await _usersController.CreateUser(It.IsAny<RegisterUserCommand>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultCreateUser);

            var validationResult = ((Response<RegisterUserCommand>)((BadRequestObjectResult)resultCreateUser).Value).Errors;
            Assert.NotNull(validationResult);
            Assert.True(validationResult.Any());
        }


        [Fact]
        public async void EditUser_WhenCalled_VerifyTheUserDataWasModified()
        {
            Response<EditUserCommand> _response = new Response<EditUserCommand>();
            _serviceUserMock
            .Setup(x => x.EditUser(It.IsAny<EditUserCommand>()))
            .Returns(() => Task.FromResult(_response));

            var resultEditUser = await _usersController.EditUser(It.IsAny<EditUserCommand>());

            Assert.IsType<OkObjectResult>(resultEditUser);

            Assert.Equal(Constants.UserEditedSucessful, ((Response<EditUserCommand>)((OkObjectResult)resultEditUser).Value).Message);
        }

        [Fact]
        public async Task EditUser_InvalidUserData_ReturnsBadRequestWithErrors()
        {
            Response<EditUserCommand> _response = new Response<EditUserCommand>();
            // Arrange
            _response.Errors = new List<string>()
            {
                Constants.UserSpecificationEmailError,
                Constants.UserSpecificationFirstNameNullEmptyOrWithSpace
            };

            _serviceUserMock
              .Setup(x => x.EditUser(It.IsAny<EditUserCommand>()))
              .Returns(() => Task.FromResult(_response));

            // Act
            var resultEditUser = await _usersController.EditUser(It.IsAny<EditUserCommand>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultEditUser);

            var validationResult = ((Response<EditUserCommand>)((BadRequestObjectResult)resultEditUser).Value).Errors;
            Assert.NotNull(validationResult);
            Assert.True(validationResult.Any());
        }

        [Fact]
        public async void DeleteUser_WhenCalled_VerifyTheUserWasDeleted()
        {

            Healthy.Domain.Responses.Response<object> _response = new Healthy.Domain.Responses.Response<object>();

            _serviceUserMock
            .Setup(x => x.DeleteUser(It.IsAny<DeleteUserCommand>()))
            .Returns(() => Task.FromResult(_response));

            var resultDeleteUser = await _usersController.DeleteUser(It.IsAny<DeleteUserCommand>());

            Assert.IsType<OkObjectResult>(resultDeleteUser);

            Assert.Equal(Constants.UserDeletedSucessful, ((Healthy.Domain.Responses.Response<object>)((OkObjectResult)resultDeleteUser).Value).Message);
        }
        [Fact]
        public async void DeleteUser_WhenCalled_ReturnsErrorAndNotDeletedUser()
        {
            Healthy.Domain.Responses.Response<object> _response = new Healthy.Domain.Responses.Response<object>();
            // Arrange
            _response.Errors = new List<string>()
            {
                Constants.UserNotExist
             };

            _serviceUserMock
              .Setup(x => x.DeleteUser(It.IsAny<DeleteUserCommand>()))
                      .Returns(() => Task.FromResult(_response));

            // Act
            var resultDeleteUser = await _usersController.DeleteUser(It.IsAny<DeleteUserCommand>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultDeleteUser);

            var validationResult = ((Healthy.Domain.Responses.Response<object>)((BadRequestObjectResult)resultDeleteUser).Value).Errors;
            
            Assert.NotNull(validationResult);
            Assert.True(validationResult.Any());
        }
    }
}
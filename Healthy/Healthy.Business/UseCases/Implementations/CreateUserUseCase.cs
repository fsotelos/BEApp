using Healthy.Business.Commands;
using Healthy.Business.Specifications.Definitions;
using Healthy.Business.UseCases.Definitions;
using Healthy.Domain.Responses;
using Healthy.Infraestructure.Repository.Definitions;
using Healthy.Utilities.Security;

namespace Healthy.Business.UseCases.Implementations
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserSpecification _createUserSpecification;
        private readonly IUserRepostory _userRepostory;

        public CreateUserUseCase(IUserSpecification createUserSpecification, IUserRepostory userRepostory)
        {
            _createUserSpecification = createUserSpecification;
            _userRepostory = userRepostory;
        }
        public async Task<Response<RegisterUserCommand>> Execute(RegisterUserCommand registerUserCommand)
        {
            var resultValidation = _createUserSpecification.Valid(registerUserCommand).ToList();

            if (!resultValidation.Any())
            {
                registerUserCommand.FormatDates();
                registerUserCommand.Password = SecurityManager.Encrypt(registerUserCommand.Password);
                await _userRepostory.CreateUser(registerUserCommand);
            }

            return new Response<RegisterUserCommand> { Errors = resultValidation, Result = new List<RegisterUserCommand>() { registerUserCommand } };
        }
    }
}

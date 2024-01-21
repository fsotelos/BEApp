using Healthy.Business.Commands;
using Healthy.Business.UseCases.Definitions;
using Healthy.Domain.Constants;
using Healthy.Domain.Responses;
using Healthy.Infraestructure.Repository.Definitions;

namespace Healthy.Business.UseCases.Implementations
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepostory _userRepostory;

        public DeleteUserUseCase(IUserRepostory userRepostory)
        {
            _userRepostory = userRepostory;
        }
        public async Task<Response<object>> Execute(DeleteUserCommand deleteUserCommand)
        {
            List<string> errors = new List<string>();
            var userExist = _userRepostory.GetUserById(deleteUserCommand.Id);
            if (userExist is not null)
            {
                await _userRepostory.DeleteUser(userExist);
            }
            else
            {
                errors.Add(Constants.UserNotExist);
            }

            return new Response<object> { Errors = errors, Result = new List<object>() };
        }
    }
}

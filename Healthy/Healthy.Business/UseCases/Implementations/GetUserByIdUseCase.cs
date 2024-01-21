using Healthy.Business.UseCases.Definitions;
using Healthy.Domain.Entities;
using Healthy.Domain.Responses;
using Healthy.Infraestructure.Repository.Definitions;

namespace Healthy.Business.UseCases.Implementations
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        IUserRepostory _userRepostory;

        public GetUserByIdUseCase(IUserRepostory userRepostory)
        {
            _userRepostory = userRepostory;
        }
        public async Task<Response<User>> Execute(User user = null)
        {
            Response<User> response = new Response<User>();
            response.Result = new List<User>() { _userRepostory.GetUserById(user.Id) };
            return response;
        }
    }
}

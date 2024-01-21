using Healthy.Business.UseCases.Definitions;
using Healthy.Domain.Entities;
using Healthy.Domain.Responses;
using Healthy.Infraestructure.Repository.Definitions;

namespace Healthy.Business.UseCases.Implementations
{
    public class GetAllUsersUseCase : IGetAllUsersUseCase
    {
        private IUserRepostory _userRepostory;

        public GetAllUsersUseCase(IUserRepostory userRepostory)
        {
            _userRepostory = userRepostory;
        }
        public async Task<Response<User>> Execute(User user = null)
        {
            Response<User> response = new Response<User>();
            response.Result = await _userRepostory.GetUsers();
            return response;
        }
    }
}

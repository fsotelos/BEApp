using Healthy.Domain.Entities;
using Healthy.Domain.Responses;

namespace Healthy.Business.UseCases.Definitions
{
    public interface IGetAllUsersUseCase :  IUseCase<User>
    {
        Task<Response<User>> Execute(User input = null);
    }
}
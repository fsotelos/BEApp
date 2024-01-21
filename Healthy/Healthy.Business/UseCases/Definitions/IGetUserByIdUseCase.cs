using Healthy.Domain.Entities;
using Healthy.Domain.Responses;

namespace Healthy.Business.UseCases.Definitions
{
    public interface IGetUserByIdUseCase
    {
        Task<Response<User>> Execute(User user = null);
    }
}
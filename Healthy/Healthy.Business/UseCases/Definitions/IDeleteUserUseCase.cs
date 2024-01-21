using Healthy.Business.Commands;
using Healthy.Domain.Responses;

namespace Healthy.Business.UseCases.Definitions
{
    public interface IDeleteUserUseCase
    {
        Task<Response<object>> Execute(DeleteUserCommand registerUserCommand);
    }
}
using Healthy.Domain.Entities;
using Healthy.Domain.Responses;

namespace Healthy.Business.UseCases.Definitions
{
    public interface IUseCase<T>
    {
        Task<Response<T>> Execute(T input = default(T));
    }
}
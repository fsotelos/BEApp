using Healthy.Domain.Entities;
using Healthy.Domain.Responses;
using MediatR;

namespace Healthy.Business.Commands
{
    public class RegisterUserCommand : User, IRequest<Response<RegisterUserCommand>>
    {

    }
}

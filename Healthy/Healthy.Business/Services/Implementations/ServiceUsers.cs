using Healthy.Business.Commands;
using Healthy.Business.Services.Definitions;
using Healthy.Domain.Entities;
using Healthy.Domain.Queries;
using Healthy.Domain.Responses;
using MediatR;

namespace Healthy.Business.Services.Implementations
{
    public class ServiceUsers : IServiceUsers
    {
        private readonly IMediator _mediator;

        public ServiceUsers(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task<Response<RegisterUserCommand>> CreateUser(RegisterUserCommand registerUserCommand)
        {
            var result = await this._mediator.Send<Response<RegisterUserCommand>>(registerUserCommand);
            return result;
        }

        public async Task<Response<EditUserCommand>> EditUser(EditUserCommand editUserCommand)
        {
            var result = await this._mediator.Send<Response<EditUserCommand>>(editUserCommand);
            return result;
        }

        public async Task<Response<object>> DeleteUser(DeleteUserCommand deleteUserCommand)
        {
            var result = await this._mediator.Send<Response<object>>(deleteUserCommand);
            return result;
        }

        public async Task<Response<User>> GetUsers(GetUsersQuery getUserQuery)
        {
            var result = await this._mediator.Send<Response<User>>(getUserQuery);
            return result;
        }

        public async Task<Response<User>> GetUserById(GetUserByIdQuery getUserIdQuery)
        {
            var result = await this._mediator.Send<Response<User>>(getUserIdQuery);
            return result;
        }
    }
}

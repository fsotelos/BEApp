using Healthy.Business.Commands;
using Healthy.Domain.Entities;
using Healthy.Domain.Queries;
using Healthy.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthy.Business.Services.Definitions
{
    public interface IServiceUsers
    {
        Task<Response<RegisterUserCommand>> CreateUser(RegisterUserCommand userRegisterCommand);
        Task<Response<EditUserCommand>> EditUser(EditUserCommand editUserCommand);
        Task<Response<object>> DeleteUser(DeleteUserCommand deleteUserCommand);
        Task<Response<User>> GetUsers(GetUsersQuery getUserQuery);

        Task<Response<User>> GetUserById(GetUserByIdQuery getUserByIdQuery);
    }
}

using Healthy.Business.Commands;
using Healthy.Domain.Entities;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthy.Infraestructure.Repository.Definitions
{
    public  interface IUserRepostory
    {
        Task CreateUser(RegisterUserCommand registerUserCommand);
        Task UpdateUser(EditUserCommand editUserCommand);
        Task DeleteUser(User deleteUserCommand);   
        User GetUserById(int id);
        Task<List<User>> GetUsers();
    }
}

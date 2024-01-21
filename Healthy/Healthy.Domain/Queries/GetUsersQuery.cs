using Healthy.Domain.Entities;
using Healthy.Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthy.Domain.Queries
{
    public class GetUsersQuery : IRequest<Response<User>>
    {

    }
}

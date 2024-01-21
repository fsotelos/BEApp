using Healthy.Domain.Entities;
using Healthy.Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Healthy.Business.Commands
{
    public class DeleteUserCommand : IRequest<Response<object>>
    {
        public int Id { get; set; }
    }
}

using Healthy.Business.Commands;
using Healthy.Business.UseCases.Definitions;
using Healthy.Domain.Entities;
using Healthy.Domain.Queries;
using Healthy.Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthy.Business.QueryHandlers
{
    public class GetAllUsersHandler : IRequestHandler<GetUsersQuery, Response<User>>
    {
        IGetAllUsersUseCase _getAllUsersUseCase;
        public GetAllUsersHandler(IGetAllUsersUseCase getAllUsersUseCase)
        {
            _getAllUsersUseCase = getAllUsersUseCase;
        }
        public async Task<Response<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _getAllUsersUseCase.Execute();
        }
    }
}

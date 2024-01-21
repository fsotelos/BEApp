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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Response<User>>
    {
        IGetUserByIdUseCase _getUserByIdUseCase;
        public GetUserByIdHandler(IGetUserByIdUseCase getUserByIdUseCase)
        {
            _getUserByIdUseCase = getUserByIdUseCase;
        }
        public async Task<Response<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = new() { Id = request.Id };
            return await _getUserByIdUseCase.Execute(user);
        }
    }
}

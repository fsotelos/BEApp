using Healthy.Business.Commands;
using Healthy.Business.UseCases.Definitions;
using Healthy.Domain.Responses;
using MediatR;

namespace Healthy.Business.CommandHandlers
{
    public class RemoveUserHandler : IRequestHandler<DeleteUserCommand, Response<object>>
    {
        private IDeleteUserUseCase _deleteUserUseCase;
        public RemoveUserHandler(IDeleteUserUseCase deleteUserUseCase)
        {
            _deleteUserUseCase = deleteUserUseCase;
        }
        public async Task<Response<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _deleteUserUseCase.Execute(request);
        }
    }
}

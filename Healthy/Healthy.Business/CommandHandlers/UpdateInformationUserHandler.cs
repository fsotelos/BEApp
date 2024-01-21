using Healthy.Business.Commands;
using Healthy.Business.UseCases.Definitions;
using Healthy.Domain.Responses;
using MediatR;

namespace Healthy.Business.CommandHandlers
{
    public class UpdateInformationUserHandler : IRequestHandler<EditUserCommand, Response<EditUserCommand>>
    {
        private IEditUserUseCase _editUserUseCase;
        public UpdateInformationUserHandler(IEditUserUseCase editUserUseCase)
        {
            _editUserUseCase = editUserUseCase;
        }
        public async Task<Response<EditUserCommand>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
           return await _editUserUseCase.Execute(request);
        }
    }
}

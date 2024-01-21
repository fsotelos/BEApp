using Healthy.Business.Commands;
using Healthy.Business.UseCases.Definitions;
using Healthy.Domain.Responses;
using MediatR;

namespace Healthy.Business.CommandHandlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Response<RegisterUserCommand>>
    {
        private ICreateUserUseCase _createUserUseCase;
        public RegisterUserHandler(ICreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }
        public async Task<Response<RegisterUserCommand>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _createUserUseCase.Execute(request);
        }
    }
}

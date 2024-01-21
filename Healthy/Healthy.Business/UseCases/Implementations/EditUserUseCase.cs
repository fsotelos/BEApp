using Healthy.Business.Commands;
using Healthy.Business.Specifications.Definitions;
using Healthy.Business.UseCases.Definitions;
using Healthy.Domain.Responses;
using Healthy.Infraestructure.Repository.Definitions;

namespace Healthy.Business.UseCases.Implementations
{
    public class EditUserUseCase : IEditUserUseCase
    {
        private readonly IUserRepostory _userRepostory;
        private readonly IUserSpecification _userSpecification;
        public EditUserUseCase(IUserSpecification userSpecification, IUserRepostory userRepostory) {
            _userRepostory = userRepostory;
            _userSpecification = userSpecification;
        }
        public async Task<Response<EditUserCommand>> Execute(EditUserCommand editUserCommand)
        {
            var resultsValidation = _userSpecification.Valid(editUserCommand).ToList();

            if (!resultsValidation.Any())
            {
                editUserCommand.FormatDates();
                await _userRepostory.UpdateUser(editUserCommand);
            }
            return new Response<EditUserCommand> { Errors = resultsValidation, Result = new List<EditUserCommand>() { editUserCommand } };
        }
    }
}

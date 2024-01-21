using Healthy.Business.Commands;
using Healthy.Business.Services.Definitions;
using Healthy.Domain.Constants;
using Healthy.Domain.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.WebAPI.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IServiceUsers _serviceUsers;
        public UsersController(IServiceUsers serviceUsers)
        {
            _serviceUsers = serviceUsers;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                GetUsersQuery request = new GetUsersQuery();
                var resultCrearUsuario = await _serviceUsers.GetUsers(request);

                if (resultCrearUsuario.Errors.Any())
                    return BadRequest(resultCrearUsuario);

                resultCrearUsuario.Message = Constants.GetUsersCallSucessful;

                return Ok(resultCrearUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                GetUserByIdQuery request = new GetUserByIdQuery();
                request.Id = id;
                var resultCrearUsuario = await _serviceUsers.GetUserById(request);

                if (resultCrearUsuario.Errors.Any())
                    return BadRequest(resultCrearUsuario);

                resultCrearUsuario.Message = Constants.GetUsersCallSucessful;

                return Ok(resultCrearUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserCommand userRegisterCommand)
        {   
            try
            {
                var resultCrearUsuario = await _serviceUsers.CreateUser(userRegisterCommand);

                if (resultCrearUsuario.Errors.Any())
                    return BadRequest(resultCrearUsuario);

                resultCrearUsuario.Message = Constants.UserCreatedSucessful;

                return Ok(resultCrearUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand editUserCommand)
        {
            var resultEditUsuario = await _serviceUsers.EditUser(editUserCommand);

            if (resultEditUsuario.Errors.Any())
                return BadRequest(resultEditUsuario);

            resultEditUsuario.Message = Constants.UserEditedSucessful;

            return Ok(resultEditUsuario);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand deleteUserCommand)
        {
            var resultEditUsuario = await _serviceUsers.DeleteUser(deleteUserCommand);

            if (resultEditUsuario.Errors.Any())
                return BadRequest(resultEditUsuario);

            resultEditUsuario.Message = Constants.UserDeletedSucessful;

            return Ok(resultEditUsuario);
        }
    }
}

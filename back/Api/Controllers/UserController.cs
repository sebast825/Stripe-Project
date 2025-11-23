using Core.Dto.User;
using Core.Entities;
using Aplication.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aplication.UseCases.Users;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;

        public UserController(CreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost]
        public async Task<ActionResult>  Create([FromBody] UserCreateRequestDto userCreateDto)
        {
           
                await _createUserUseCase.ExecuteAsync(userCreateDto);
                return Ok();
          
        }
    }
}

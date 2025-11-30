using Core.Dto.User;
using Core.Entities;
using Aplication.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aplication.UseCases.Users;
using Aplication.Services;
using Aplication.Dto;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly IUserService _userService;

          public UsersController(CreateUserUseCase createUserUseCase, IUserService userService)
        {
            _createUserUseCase = createUserUseCase;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult>  Create([FromBody] UserCreateRequestDto userCreateDto)
        {
           
                await _createUserUseCase.ExecuteAsync(userCreateDto);
                return Ok();
          
        }
        [HttpGet]
        public async Task<ActionResult<PagedResponseDto<UserWithSubscriptionResponseDto>>> GetPaginated(
            [FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? searchTerm)
        {

            var rsta = await _userService.GetPagedAsync(page,pageSize,searchTerm);
          
            return Ok(rsta);

        }
    }
}

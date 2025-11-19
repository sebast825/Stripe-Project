using Core.Dto.User;
using Core.Entities;
using Aplication.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServicesI;
        public UserController(IUserService userServicesI) {
            _userServicesI = userServicesI;

        }
        [HttpPost]
        public async Task<ActionResult>  Create([FromBody] UserCreateRequestDto userCreateDto)
        {
           
                await _userServicesI.AddAsync(userCreateDto);
                return Ok();
          
        }
    }
}

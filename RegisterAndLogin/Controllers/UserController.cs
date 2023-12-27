using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterAndLoginAPI_BL.Dtos;
using RegisterAndLoginAPI_BL.Services;

namespace RegisterAndLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public ActionResult<string> Register([FromBody]UserDTO userDto)
        {
           return _userService.Register(userDto);
        }

        [HttpPost("login")]
        public ActionResult<UserResponseDTO> Login([FromForm] string email, [FromForm] string password) 
        {
            var response = _userService.Login(email, password);
            if (response.Status == Status.NOT_FOUND) return BadRequest("user not found");
            if(response.Status == Status.INVALID) return Unauthorized("Email or password aren't correct");
            return Ok(response);
        }
    }
}

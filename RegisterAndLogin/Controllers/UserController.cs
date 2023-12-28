using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterAndLoginAPI_BL.Dtos;
using RegisterAndLoginAPI_BL.Services;
using RegisterAndLoginAPI_DAL.Models;

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

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<string> Register([FromBody]UserDTO userDto)
        {
           return _userService.Register(userDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<UserResponseDTO> Login([FromForm] string email, [FromForm] string password) 
        {
            var response = _userService.Login(email, password);
            if (response.Status == Status.NOT_FOUND) return BadRequest("user not found");
            if(response.Status == Status.INVALID) return Unauthorized("Email or password aren't correct");
            return Ok(response);
        }

        [Authorize (Roles = "admin")]
        [HttpGet("users")]
        public ActionResult<ICollection<User>> GetUsers()
        {
            ICollection<User>? users = _userService.GetUsers();
            return Ok(users);
        }
    }
}

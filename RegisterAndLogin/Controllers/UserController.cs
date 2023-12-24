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

    }
}

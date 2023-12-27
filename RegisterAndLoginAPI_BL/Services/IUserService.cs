using RegisterAndLoginAPI_BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterAndLoginAPI_BL.Services
{
    public interface IUserService
    {
        public string Register(UserDTO userDto);
        public UserResponseDTO Login(string email, string password);
        
    }
}

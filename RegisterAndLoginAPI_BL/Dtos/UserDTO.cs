using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterAndLoginAPI_BL.Dtos
{
    public class UserDTO
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }

        public UserDTO(string nickname, string email, string password, string repeatedPassword) 
        {
            NickName = nickname;
            Email = email;
            Password = password;
            RepeatedPassword = repeatedPassword;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RegisterAndLoginAPI_DAL.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordSalt { get; set; }
        public string Rol { get; set; } = "normal";

        public User()
        {

        }

        public User(string nickname, string email)
        {
            NickName = nickname;
            Email = email;
            PasswordHash = new byte[0];
            PasswordSalt = new Byte[0];
        }
    }
}

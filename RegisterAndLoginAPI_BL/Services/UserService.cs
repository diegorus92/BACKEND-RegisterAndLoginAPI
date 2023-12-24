using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RegisterAndLoginAPI_BL.Dtos;
using RegisterAndLoginAPI_DAL.Data;
using RegisterAndLoginAPI_DAL.Models;

namespace RegisterAndLoginAPI_BL.Services
{
    public class UserService: IUserService
    {
        private readonly Context _context;

        public UserService(Context context) 
        {
            _context = context;
        }


        public string Register(UserDTO userDto)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if (user != null) return "User already exist";

            if (userDto.Password != userDto.RepeatedPassword) return "The password doesn't match with repeated password";

            User newUser;
            newUser = new User(userDto.NickName, userDto.Email);
            EncryptPassword(newUser, userDto.Password);

            _context.Users.AddAsync(newUser);
            _context.SaveChangesAsync();
            _context.DisposeAsync();

            return "User registration successful!";
        }




        //PRIVATED METHODS//////
        private void EncryptPassword(User user, string password)
        {
            var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            user.PasswordSalt = hmac.Key;

        }
    }
}

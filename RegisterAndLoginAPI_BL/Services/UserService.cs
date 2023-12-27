using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using RegisterAndLoginAPI_BL.Dtos;
using RegisterAndLoginAPI_DAL.Data;
using RegisterAndLoginAPI_DAL.Models;

namespace RegisterAndLoginAPI_BL.Services
{
    public class UserService: IUserService
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public UserService(Context context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }

        //TEST Pass: 1234

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


        public UserResponseDTO? Login(string email, string password)
        {
            User? user = _context.Users.FirstOrDefault( u => u.Email.ToLower() == email.ToLower());
            if (user == null) return new UserResponseDTO { Status = Status.NOT_FOUND};

            if (!VerifyPassword(user, password)) return new UserResponseDTO { Status = Status.INVALID };

            var token = CreateToken(user);

            return new UserResponseDTO{ UserName = user.NickName, Token = token , Status = Status.OK};
        }



        //PRIVATED METHODS//////
        private void EncryptPassword(User user, string password)
        {
            var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            user.PasswordSalt = hmac.Key;

        }

        private bool VerifyPassword(User user, string password)
        {
            var hmac = new HMACSHA512(user.PasswordSalt);
            var encryptedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for(int i = 0; i < encryptedPassword.Length; i++) 
            {
                if (encryptedPassword[i] != user.PasswordHash[i]) 
                    return false;
            }

            return true;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

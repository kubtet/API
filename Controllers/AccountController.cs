using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Login)) return BadRequest("Login is already taken");

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Login = registerDto.Login.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Login = user.Login,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Login == loginDto.Login);

            if(user == null) return Unauthorized("Invalid login!");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password!");
            }

            return new UserDto
            {
                Login = user.Login,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<bool> UserExists(string login)
        {
            return await _context.Users.AnyAsync(x => x.Login == login.ToLower());
        }    
    }
}   
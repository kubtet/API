using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        public UsersController(DataContext context, IBookRepository bookRepository, IGenreRepository genreRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            // Get the authenticated user's ID
            //var authenticatedUserId = User.FindFirst(ClaimTypes.NameIdentifier);

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                // If the user is not found, return a not found response
                return NotFound();
            }

            // Return the user
            return user;
        }


    }
}
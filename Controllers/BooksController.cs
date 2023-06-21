using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BooksController:ControllerBase
    {

        private readonly DataContext _context;
        public BooksController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<User>> GetBooks()
        {
            return await _context.Users.FindAsync(id);
        }
    }
}

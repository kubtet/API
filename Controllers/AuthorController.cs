using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthorController : BaseApiController
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAll()
        {
            return await _authorRepository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var author = await _authorRepository.GetById(id);
            if(author == null)
            {
                return NotFound();
            }
            return author;
        }
    }
}
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthorController : BaseApiController
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
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
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]AuthorDto authorCreate)
        {
            if(authorCreate == null)
            {
                return BadRequest(ModelState);
            }
            if(await _authorRepository.AuthorExists(authorCreate.Name,authorCreate.Surname,authorCreate.Country))
            {
                ModelState.AddModelError("Error","Author already exists");
                return BadRequest(ModelState);
            }
            var author = _mapper.Map<Author>(authorCreate);
            var result = await _authorRepository.Create(author);
            if(result>=0)
            {
                return Ok(await _authorRepository.GetById(result));
            }
            return BadRequest(ModelState);

        }

    }
}
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GenreController:BaseApiController
    {
        private IGenreRepository _genreRepository;
        private IMapper _mapper;
        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var genres = await _genreRepository.GetAll();
            return Ok(genres);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute]int id)
        {
            var genre = await _genreRepository.GetById(id);
            if(genre == null)
            {
                return NotFound(genre.Id);
            }
            return Ok(genre);
        }
        [HttpPost]
        public async Task<IActionResult> create([FromBody]GenreDto genreCreate)
        {
            if(genreCreate == null)
            {
                return BadRequest(ModelState);
            }
            if(await _genreRepository.GenreExists(genreCreate.Name))
            {
                ModelState.AddModelError("Error","Genre already exists");
                return BadRequest(ModelState);
            }
            var genre = _mapper.Map<Genre>(genreCreate);
            var result = await _genreRepository.Create(genre);
            if(result>= 0)
            {
                return Ok(await _genreRepository.GetById(result));
            }
            return BadRequest(ModelState);
        }
    }

}

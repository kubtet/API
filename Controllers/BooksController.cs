using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API.Controllers
{
    public class BooksController : BaseApiController
    {

        private IBookRepository _bookRepository;
        private IMapper _mapper;
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var books = await _bookRepository.GetAll();

            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getBookById([FromRoute] int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null) {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpGet("getByTitle/{title}")]
        public async Task<IActionResult> getBookByTitle([FromRoute] string title)
        {
            var book = await _bookRepository.GetBooksByTitle(title);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> create([FromBody] BookCreateDto bookCreate)
        {
            if (bookCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (bookCreate.GenresId.Count == 0)
            {
                ModelState.AddModelError("Error", "Book must have at least one genre");
                return BadRequest(ModelState);
            }
            if (await _bookRepository.BookExists(bookCreate.Title, bookCreate.Isbn))
            {
                ModelState.AddModelError("Error", "Book already exists");
                return BadRequest(ModelState);
            }
            var book = _mapper.Map<Book>(bookCreate);
            var result = await _bookRepository.Create(book, bookCreate.GenresId);
            if (result == -2)
            {
                ModelState.AddModelError("Error", "Author doesn't exist");
            }
            if (result == -3)
            {
                ModelState.AddModelError("Error", "Bad genres");
            }
            if (ModelState.IsValid)
            {
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpPost("{BookId}/AddCover")]
        public async Task<IActionResult> AddCover([FromForm] IFormFile file, [FromRoute] int bookId)
        {

            var result = await _bookRepository.AddCover(file, bookId);
            if(result==-4) {
                ModelState.AddModelError("Error", "File should be uploaded");
                return BadRequest(ModelState);
            }
            if(result == -3)
            {
                ModelState.AddModelError("Error", "Incorrect file format");
                return BadRequest(ModelState);
            }
            if (result == -2)
            {
                ModelState.AddModelError("Error", "Wrong bookId");

                return BadRequest(ModelState);
            }
            if(result == -1)
            {
                ModelState.AddModelError("Error", "allowed file formats jpg,png,jpeg");
            }
            return Ok();
            
        }
        [HttpPost]
        [Route("{BookId}/AddGenre/{GenreId}")]
        public async Task<IActionResult> addGenre([FromRoute]int BookId, [FromRoute]int GenreId)
        {
            if(_bookRepository.GetById(BookId)==null)
            {
                ModelState.AddModelError("Error","Book doesn't exist");
                return BadRequest(ModelState);
            }
            if(await _bookRepository.AddGenreToBook(BookId,GenreId))
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}

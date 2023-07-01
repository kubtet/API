using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

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
            if(result== -1) {
                ModelState.AddModelError("Error", "Something went wrong");
                return BadRequest(ModelState);
            }
            if(result == -3)
            {
                ModelState.AddModelError("Error", "File not provided");
                return BadRequest(ModelState);
            }
            if (result == -2)
            {
                ModelState.AddModelError("Error", "Wrong bookId");

                return BadRequest(ModelState);
            }
            if(result == -4)
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
        [HttpPut]
        [Route("{BookId}/Like")]
        [Authorize]
        public async Task<IActionResult> likeBook([FromRoute]int BookId)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _bookRepository.likeBook(userName, BookId))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("{BookId}/Read")]
        [Authorize]
        public async Task<IActionResult> addToRead([FromRoute]int BookId, [FromBody] ReadBookDto readBookDto)
        {
            if(readBookDto == null)
            {
                ModelState.AddModelError("Error", "Rating and comment are required");
                return BadRequest(ModelState);
            }
            if(readBookDto.Rating < 1 || readBookDto.Rating > 5)
            {
                ModelState.AddModelError("Error", "Rating must be between 1 and 5");
                return BadRequest(ModelState);
            }
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _bookRepository.AddToRead(userName, BookId, readBookDto.Rating, readBookDto.Comment))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("{BookId}/Read")]
        [Authorize]
        public async Task<IActionResult> deleteFromRead([FromRoute]int BookId)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _bookRepository.DeleteFromRead(userName, BookId))
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}

using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API.Controllers
{
    public class BooksController: BaseApiController
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
        public async Task<IActionResult> getBookById([FromRoute]int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null) { 
                return NotFound();
            }
            return Ok(book);
        }
    }
}

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

        private readonly DataContext _context;
        private IBookRepository _bookRepository;
        private IMapper _mapper;
        public BooksController(DataContext context,IBookRepository bookRepository, IMapper mapper)
        {
            _context = context;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            var books = _bookRepository.GetAll();
            var booksdto = _mapper.Map<List<BookDto>>(books);
            return Ok(booksdto);
        }

    }
}

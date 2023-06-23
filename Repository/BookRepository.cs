using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BookRepository(DataContext dataContext,IMapper mapper)
        {
            _context = dataContext;   
            _mapper = mapper;
        }
        public bool BookExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookDto>> GetAll()
        {
             return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.LikedBooks)
                .Include(b=>b.ReadBooks)
                .Include(b=>b.BookGenres)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BookDetailsDto> GetById(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.LikedBooks).ThenInclude(lb=>lb.User)
                .Include(b => b.ReadBooks)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null; // Or return an appropriate response indicating that the book was not found
            }

            // Map the book to BookDetailsDto
            var bookDto = _mapper.Map<BookDetailsDto>(book);

            return bookDto;
        }

    }
}

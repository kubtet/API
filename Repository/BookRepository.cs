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
                return null; 
            }

            // Map the book to BookDetailsDto
            var bookDto = _mapper.Map<BookDetailsDto>(book);

            return bookDto;
        }
        // Create a book
        // Return -2 if the author does not exist
        // Return -3 if the book's genres were not added to the database
        // Return 0 if the book was not created
        // Return 1 if the book was created
        //
        public async Task<int> Create(Book book,List<int> genresId)
        {
            // Add the book to the database
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == book.AuthorId);
            if(author == null)
            {
                return -2;
            }
            _context.Books.Add(book);
            foreach (var genreId in genresId)
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
                if(genre == null)
                {
                    return -3;
                }
                BookGenre bookGenre = new BookGenre
                {
                    Book= book,
                    Genre = genre
                };
                _context.BooksGenres.Add(bookGenre);
            }
           return await _context.SaveChangesAsync();

        }
        public async Task<Boolean> BookExists(string title, string isbn)
        {
            return await _context.Books
                .AnyAsync(b => b.Title.ToUpper() == title.ToUpper()
                    && b.Isbn.ToUpper() == isbn.ToUpper());
        }
        public async Task<Boolean> AddGenreToBook(int bookId, int genreId)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
            if(genre == null)
            {
                return false;
            }
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if(book == null)
            {
                return false;
            }
            BookGenre bookGenre = new BookGenre
            {
                Book = book,
                Genre = genre
            };
            _context.BooksGenres.Add(bookGenre);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}

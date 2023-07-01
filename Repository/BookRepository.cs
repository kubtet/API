using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using API.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        private readonly IBookImageService _bookImageService;
        private readonly IMapper _mapper;
        public BookRepository(DataContext dataContext,IMapper mapper,IBookImageService bookImageService)
        {
            _context = dataContext;   
            _mapper = mapper;
            _bookImageService = bookImageService;
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
        // Return -4 if the image file format is wrong
        // Return -5 if the image is not uploaded
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
        public async Task<List<BookDto>> GetBooksByTitle(string title)
        {
            var books = await _context.Books
                .Where(b => b.Title.Contains(title))
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                .ToListAsync();
            if (books == null)
            {
                return null;
            }      
            return _mapper.Map<List<BookDto>>(books);
        }
        //return 0 file added
        //return -1 file not added
        //return -2 bookId is wrong
        //return -3 file is null
        //return -4 wrong file format
        public async Task<int> AddCover(IFormFile file, int BookId)
        {
            if (file == null)
            {
                return -3;
            }
            if (!_bookImageService.CorrectFileFormat(file))
            {
                return -4;
            }
            var book=_context.Books.Where(b=>b.Id== BookId).FirstOrDefault();
            if(book == null)
            {
                return -2;
            }
            var fileUploadResult = await _bookImageService.UploadImage(file);
            if (fileUploadResult == null)
            {
                return -1;
            }
            book.CoverPath = fileUploadResult;
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return 0;

        }
        public async Task<bool> likeBook(string userName, int BookId){
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == userName);
            if(user == null)
            {
                return false;
            }
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == BookId);
            if(book == null)
            {
                return false;
            }
            var like = await _context.LikedBooks.FirstOrDefaultAsync(lb => lb.BookId == BookId && lb.UserId == user.Id);
            if(like != null)
            {
                _context.LikedBooks.Remove(like);
            }
            else{
                LikedBook likedBook = new LikedBook
                {
                    Book = book,
                    User = user
                };
                _context.LikedBooks.Add(likedBook);
            }
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<Boolean> AddToRead(string userName, int BookId, int rating, string comment){
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == userName);
            if(user == null)
            {
                return false;
            }
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == BookId);
            if(book == null)
            {
                return false;
            }
            
            var read = await _context.ReadBooks.FirstOrDefaultAsync(rb => rb.BookId == BookId && rb.UserId == user.Id);
            
            if(read==null){
                ReadBook readBook = new ReadBook
                {
                    Book = book,
                    User = user,
                    Rating = rating,
                    Comment = comment
                };
                _context.ReadBooks.Add(readBook);
            }
            else{
                read.Rating = rating;
                read.Comment = comment;
                _context.ReadBooks.Update(read);
            }
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Boolean> DeleteFromRead(string userName, int BookId){
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == userName);
            if(user == null)
            {
                return false;
            }
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == BookId);
            if(book == null)
            {
                return false;
            }
            var read = await _context.ReadBooks.FirstOrDefaultAsync(rb => rb.BookId == BookId && rb.UserId == user.Id);
            if(read == null)
            {
                return false;
            }
            _context.ReadBooks.Remove(read);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

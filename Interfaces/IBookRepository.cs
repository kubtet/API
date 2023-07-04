
using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<BookDto>> GetAll();
        public Task<BookDetailsDto> GetById(int id);
        public Task<int> Create(Book book,List<int> genresId);
        public Task<int> AddCover(IFormFile file, int BookId);
        public Task<Boolean> BookExists(string title, string isbn);
        public Task<Boolean> AddGenreToBook(int BookId, int genreId);
        public Task<List<BookDto>> GetBooksByTitle(string title);
        public Task<bool> likeBook(string userName, int BookId);
        public Task<Boolean> ToRead(string userName, int BookId, int rating, string comment);
        bool BookExists(int id);
        public Task<List<BookDto>> LikedBooks(string userName);
        public Task<List<BookDto>> ReadBooks(string userName);
        public Task<Boolean> ToRead(string userName, int bookId);

        public Task<List<BookDto>> getToRead(string userName);


    }
}

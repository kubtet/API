
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
        public Task<Boolean> AddToRead(string userName, int BookId, int rating, string comment);
        public Task<Boolean> DeleteFromRead(string userName, int BookId);
        bool BookExists(int id);
    }
}

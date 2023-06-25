
using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<BookDto>> GetAll();
        public Task<BookDetailsDto> GetById(int id);
        public Task<int> Create(Book book,List<int> genresId, IFormFile file);
        public Task<Boolean> BookExists(string title, string isbn);
        public Task<Boolean> AddGenreToBook(int BookId, int genreId);
        public Task<List<BookDto>> GetBooksByTitle(string title);
        bool BookExists(int id);
    }
}

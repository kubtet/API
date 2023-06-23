
using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<BookDto>> GetAll();
        public Task<BookDetailsDto> GetById(int id);
        bool BookExists(int id);
    }
}

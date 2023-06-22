
using API.Entities;

namespace API.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book GetById(int id);
        bool BookExists(int id);
    }
}

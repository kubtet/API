using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext dataContext)
        {
            _context = dataContext;   
        }
        public bool BookExists(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

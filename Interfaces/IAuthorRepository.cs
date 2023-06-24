using API.DTO;
using API.Entities;

namespace API.Interfaces{
    public interface IAuthorRepository
    {
        public Task<List<AuthorDto>> GetAll();
        public Task<AuthorDto> GetById(int id);
        public Task<Boolean> Create(Author author);
        public Task<Boolean> AuthorExists(int id);
        public Task<Boolean> AuthorExists(string name, string surname, string country);
    }
}
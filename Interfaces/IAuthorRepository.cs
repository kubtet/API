using API.DTO;
using API.Entities;

namespace API.Interfaces{
    public interface IAuthorRepository
    {
        public Task<List<AuthorDto>> GetAll();
        public Task<AuthorDto> GetById(int id);

    }
}
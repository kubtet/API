using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IGenreRepository
    {
        public Task<List<GenreDto>> GetAll();
        public Task<GenreDto> GetById(int id);
        public Task<Boolean> Create(Genre genre);
        public Task<Boolean> GenreExists(int id);
        public Task<Boolean> GenreExists(string name);
    }
}

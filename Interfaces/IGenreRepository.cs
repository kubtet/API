using API.DTO;

namespace API.Interfaces
{
    public interface IGenreRepository
    {
        public Task<List<GenreDto>> GetAll();
        public Task<GenreDto> GetById(int id);
    }
}

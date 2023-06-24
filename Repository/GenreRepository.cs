using API.Data;
using API.DTO;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public GenreRepository(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }
        public async Task<List<GenreDto>> GetAll()
        {
            return await _context.Genres
                .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GenreDto> GetById(int id)
        {
            return await _context.Genres.Where(g => g.Id == id)
                .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        }
    }
}

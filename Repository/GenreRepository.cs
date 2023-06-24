using API.Data;
using API.DTO;
using API.Entities;
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
        public async Task<Boolean> Create(Genre genre)
        {
            _context.Genres.Add(genre);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<Boolean> GenreExists(int id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }
        public async Task<Boolean> GenreExists(string name)
        {
            return await _context.Genres
            .AnyAsync(g => g.Name.ToUpper() == name.ToUpper());
        }
        
    }
}

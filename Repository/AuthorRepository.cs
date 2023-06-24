using API.Data;
using API.DTO;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AuthorRepository(DataContext dataContext,IMapper mapper)
        {
            _context = dataContext;   
            _mapper = mapper;
        }

        public async Task<List<AuthorDto>> GetAll()
        {
           return await  _context.Authors
                .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AuthorDto> GetById(int id)
        {
            var author = await _context.Authors
                .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
            if(author == null)
            {
                return null;
            }
            return author;
        }
    }
}
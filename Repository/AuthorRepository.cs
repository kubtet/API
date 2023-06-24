using API.Data;
using API.DTO;
using API.Entities;
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
        public async Task<Boolean> Create(Author author)
        {
            _context.Authors.Add(author);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<Boolean> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }
        public async Task<Boolean> AuthorExists(string name, string surname, string country)
        {
            return await _context.Authors
                .AnyAsync(a => a.Name.ToUpper() == name.ToUpper()
                    && a.Surname.ToUpper() == surname.ToUpper()
                    && a.Country.ToUpper() == country.ToUpper());
        }
    }
}
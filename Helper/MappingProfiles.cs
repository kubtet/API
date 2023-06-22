using API.DTO;
using API.Entities;
using AutoMapper;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
        }
    }
}

using API.Entities;
using API.DTO;
using AutoMapper;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.UserLikes, opt => opt.MapFrom(src => src.LikedBooks.Count))
                .ForMember(dest => dest.UsersRead, opt => opt.MapFrom(src => src.ReadBooks.Count))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.BookGenres.Select(bg => bg.Genre)));

            CreateMap<Author, AuthorDto>();
            CreateMap<LikedBook, LikedBookDto>();
            CreateMap<Book, BookDetailsDto>()
                .ForMember(dest => dest.UserLikes, opt => opt.MapFrom(src => src.LikedBooks.Count))
                .ForMember(dest => dest.UsersRead, opt => opt.MapFrom(src => src.ReadBooks.Count))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.BookGenres.Select(bg => bg.Genre)));
            CreateMap<ReadBook, ReadBookDto>();
            CreateMap<BookGenre, BookGenresDto>();
            CreateMap<Genre, GenreDto>();
        }
    }
}

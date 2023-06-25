using API.Entities;
using API.DTO;
using AutoMapper;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<LikedBook, LikedBookDto>();
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.UserLikes, opt => opt.MapFrom(src => src.LikedBooks.Count))
                .ForMember(dest => dest.UsersRead, opt => opt.MapFrom(src => src.ReadBooks.Count))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.BookGenres.Select(bg => bg.Genre)))
                .ForMember(dest => dest.CoverPath, opt => opt.MapFrom(src => src.CoverPath == null ? "https://localhost:5001/uploads/default.jpg" : "https://localhost:5001/uploads/"+src.CoverPath));
            CreateMap<Book, BookDetailsDto>()
                .ForMember(dest => dest.UserLikes, opt => opt.MapFrom(src => src.LikedBooks.Count))
                .ForMember(dest => dest.UsersRead, opt => opt.MapFrom(src => src.ReadBooks.Count))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.BookGenres.Select(bg => bg.Genre)))
                .ForMember(dest => dest.CoverPath, opt => opt.MapFrom(src => src.CoverPath == null ? "https://localhost:5001/uploads/default.jpg" : "https://localhost:5001/uploads/"+src.CoverPath));
            CreateMap<BookCreateDto, Book>();
            CreateMap<ReadBook, ReadBookDto>();
            CreateMap<BookGenre, BookGenresDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
        }
    }
}

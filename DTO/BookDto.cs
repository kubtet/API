using API.Entities;

namespace API.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Cover_name { get; set; }
        public DateTime Publish_date { get; set; }
        public int UserLikes { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string UsersRead { get; set; }
        public ICollection<GenreDto> Genres { get; set; }



    }
}

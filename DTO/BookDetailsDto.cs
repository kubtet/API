using API.Entities;

namespace API.DTO
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public virtual AuthorDto Author { get; protected set; }
        public string Cover_name { get; set; }
        public string Isbn { get;  set; }
        public DateTime Publish_date { get; set; }
        public int UserLikes { get; protected set; }
        public int UsersRead { get; protected set; }
        public virtual IEnumerable<ReadBookDto> ReadBooks { get; protected set; }
        public virtual IEnumerable<GenreDto> Genres { get; protected set; }
        public virtual IEnumerable<LikedBookDto> LikedBooks { get; protected set; }


    }
}

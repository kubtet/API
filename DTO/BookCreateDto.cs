using API.Entities;

namespace API.DTO
{
    public class BookCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string Isbn { get;  set; }
        public DateTime Publish_date { get; set; }
        public List<int> GenresId { get; set; }


    }
}

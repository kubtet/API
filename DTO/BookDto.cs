using API.Entities;

namespace API.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public virtual AuthorDto Author { get; set; }

        public string Isbn { get;  set; }
        public DateTime Publish_date { get; set; }

        public virtual IEnumerable<ReadBookDto> ReadBooks { get; set; }
    }
}

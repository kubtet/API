using System.ComponentModel;

namespace API.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cover_name { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        
        public string Isbn { get; set; }
        public DateTime Publish_date { get; set; }

        public virtual ICollection<ReadBook> ReadBooks { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
        public virtual ICollection<LikedBook> LikedBooks { get; set;}
        public virtual ICollection<BooksToRead> BooksToRead { get; set; } 
    }
}

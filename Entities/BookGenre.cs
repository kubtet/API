namespace API.Entities
{
    public class BookGenre
    {
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}

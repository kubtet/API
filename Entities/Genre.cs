namespace API.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
        public virtual ICollection<UserLikesGenre> UserLikesGenres { get; set; } 
    }
}

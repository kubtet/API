namespace API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime AccountCreated { get; set; } = DateTime.Now;
        public virtual ICollection<ReadBook> ReadBooks { get; set; }
        public virtual ICollection<LikedBook> LikedBooks { get; set;}
        public virtual ICollection<BooksToRead> BooksToRead { get; set; }
        public virtual ICollection<UserLikesGenre> UserLikesGenres { get; set; }
    }
}
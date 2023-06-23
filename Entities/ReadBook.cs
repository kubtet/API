namespace API.Entities
{
    public class ReadBook
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}

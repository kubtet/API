﻿namespace API.Entities
{
    public class BooksToRead
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}

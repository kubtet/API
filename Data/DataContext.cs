using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadBook> ReadBooks { get; set; } 
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BooksGenres { get; set; }
        public DbSet<LikedBook> LikedBooks { get; set; }
        public DbSet<UserLikesGenre> UsersLikesGenres { get; set; }
        public DbSet<BooksToRead> BooksToRead { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ReadBook Relation
            modelBuilder.Entity<ReadBook>()
                .HasKey(x => new { x.UserId, x.BookId });
            modelBuilder.Entity<ReadBook>()
                .HasOne(x => x.Book)
                .WithMany(x => x.ReadBooks)
                .HasForeignKey(x => x.BookId);
            modelBuilder.Entity<ReadBook>()
                .HasOne(x => x.User)
                .WithMany(x => x.ReadBooks)
                .HasForeignKey(x => x.UserId);
            #endregion
            #region BookGenre Relation
            modelBuilder.Entity<BookGenre>()
                .HasKey(x => new { x.GenreId, x.BookId });
            modelBuilder.Entity<BookGenre>()
                .HasOne(x => x.Book)
                .WithMany(x => x.BookGenres)
                .HasForeignKey(x => x.BookId);
            modelBuilder.Entity<BookGenre>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.BookGenres)
                .HasForeignKey(x => x.GenreId);
            #endregion
            #region LikedBook Relation
            modelBuilder.Entity<LikedBook>()
                .HasKey(x => new { x.UserId, x.BookId });
            modelBuilder.Entity<LikedBook>()
                .HasOne(x => x.Book)
                .WithMany(x => x.LikedBooks)
                .HasForeignKey(x => x.BookId);
            modelBuilder.Entity<LikedBook>()
                .HasOne(x => x.User)
                .WithMany(x => x.LikedBooks)
                .HasForeignKey(x => x.UserId);
            #endregion
            #region BooksToRead Relation
            modelBuilder.Entity<BooksToRead>()
                .HasKey(x => new { x.UserId, x.BookId });
            modelBuilder.Entity<BooksToRead>()
                .HasOne(x => x.Book)
                .WithMany(x => x.BooksToRead)
                .HasForeignKey(x => x.BookId);
            modelBuilder.Entity<BooksToRead>()
                .HasOne(x => x.User)
                .WithMany(x => x.BooksToRead)
                .HasForeignKey(x => x.UserId);
            #endregion
            #region UserLikes Genre Relation
            modelBuilder.Entity<UserLikesGenre>()
                .HasKey(x => new { x.UserId, x.GenreId });
            modelBuilder.Entity<UserLikesGenre>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.UserLikesGenres)
                .HasForeignKey(x => x.GenreId);
            modelBuilder.Entity<UserLikesGenre>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserLikesGenres)
                .HasForeignKey(x => x.UserId);
            #endregion
            #region User Configure
            modelBuilder.Entity<User>()
                .Property(b => b.AccountCreated)
                .HasDefaultValueSql("getdate()");

            #endregion
        }
    }
}
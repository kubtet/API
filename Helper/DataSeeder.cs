using API.Data;
using API.Entities;
using System.Security.Cryptography;
using System.Text;

namespace API.Helper;
/*
 Its not used unless this:
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();

    // Apply pending migrations to the database (if any)
    context.Database.Migrate();

    // Seed the data
    var seeder = new DataSeeder(context);
    seeder.SeedData();
}
added to Program.cs
 */
public class DataSeeder
{
    private readonly DataContext _context;

    public DataSeeder(DataContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        // Add your seeding logic here

        // Example: Add authors and books
        SeedAuthorsAndBooks();
        _context.SaveChanges();

        // Example: Add genres
        SeedGenres();
        _context.SaveChanges();

        // Example: Add users, liked books, read books, books to read, and user likes genres
        SeedUsers();
        _context.SaveChanges();

        // Save changes to the database
    }

    private void SeedAuthorsAndBooks()
    {
        var author1 = new Author
        {
            Name = "John",
            Surname = "Doe",
            Country = "USA"
        };

        var author2 = new Author
        {
            Name = "Jane",
            Surname = "Smith",
            Country = "UK"
        };

        var book1 = new Book
        {
            Title = "Book 1",
            Description = "Description for Book 1",
            Publish_date = DateTime.Now,
            Author = author1,
            Isbn = "123456789"
        };

        var book2 = new Book
        {
            Title = "Book 2",
            Description = "Description for Book 2",
            Publish_date = DateTime.Now,
            Author = author2,
            Isbn = "987654321"
        };

        author1.Books = new List<Book> { book1 };
        author2.Books = new List<Book> { book2 };

        _context.Authors.AddRange(author1, author2);
        _context.Books.AddRange(book1, book2);
    }

    private void SeedGenres()
    {
        var genre1 = new Genre
        {
            Name = "Fantasy"
        };

        var genre2 = new Genre
        {
            Name = "Mystery"
        };

        _context.Genres.AddRange(genre1, genre2);
    }

    private void SeedUsers()
    {
        var user1 = new User
        {
            Login = "user1",
            PasswordHash = GetPasswordHash("password1"),
            PasswordSalt = GetPasswordSalt(),
            Birthday = new DateTime(1990, 1, 1),
            AccountCreated = DateTime.Now
        };

        var user2 = new User
        {
            Login = "user2",
            PasswordHash = GetPasswordHash("password2"),
            PasswordSalt = GetPasswordSalt(),
            Birthday = new DateTime(1995, 5, 5),
            AccountCreated = DateTime.Now
        };

        var book1 = _context.Books.First();
        var book2 = _context.Books.OrderBy(x=>x.Id).Last();

        var likedBook1 = new LikedBook
        {
            User = user1,
            Book = book1
        };

        var likedBook2 = new LikedBook
        {
            User = user2,
            Book = book2
        };

        var readBook1 = new ReadBook
        {
            Comment = "Great book!",
            Rating = 5,
            User = user1,
            Book = book1
        };

        var readBook2 = new ReadBook
        {
            Comment = "Interesting read.",
            Rating = 4,
            User = user2,
            Book = book2
        };

        var booksToRead1 = new BooksToRead
        {
            User = user1,
            Book = book2
        };

        var booksToRead2 = new BooksToRead
        {
            User = user2,
            Book = book1
        };

        var genre1 = _context.Genres.First();
        var genre2 = _context.Genres.OrderBy(x=>x.Id).Last();

        var userLikesGenre1 = new UserLikesGenre
        {
            User = user1,
            Genre = genre1
        };

        var userLikesGenre2 = new UserLikesGenre
        {
            User = user2,
            Genre = genre2
        };

        _context.Users.AddRange(user1, user2);
        _context.LikedBooks.AddRange(likedBook1, likedBook2);
        _context.ReadBooks.AddRange(readBook1, readBook2);
        _context.BooksToRead.AddRange(booksToRead1, booksToRead2);
        _context.UsersLikesGenres.AddRange(userLikesGenre1, userLikesGenre2);
    }

    private byte[] GetPasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private byte[] GetPasswordSalt()
    {
        using var hmac = new HMACSHA512();
        return hmac.Key;
    }
}

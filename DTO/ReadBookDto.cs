using API.Entities;

namespace API.DTO
{
    public class ReadBookDto
    {
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}

namespace API.Interfaces
{
    public interface IBookImageService
    {
       public Tuple<int,string> UploadImage(IFormFile file);
    }
}
namespace API.Interfaces
{
    public interface IBookImageService
    {
        public Task<string> UploadImage(IFormFile file);
        public bool CorrectFileFormat(IFormFile file);
    }
}
using System.Security.Cryptography;
using API.Interfaces;

namespace API.Services{
    public class BookImageService : IBookImageService
    {
        //return -1 unhandled error
        //return -2 incorrect file format
        //return 0 file
        public bool CorrectFileFormat(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string[] allowedExtensions = { ".jpg", ".png", ".jpeg" };
            if (!allowedExtensions.Contains(extension.ToLower()))
            {
                return false;
            }
            return true;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName);

            var fileMD5 ="";
            using (var md5 = MD5.Create())
            {
                using (var stream = file.OpenReadStream())
                {
                    var hash = md5.ComputeHash(stream);
                    fileMD5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
            string path = Path.Combine(Directory.GetCurrentDirectory(),"uploads",fileMD5+"."+extension);
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch(Exception e){
                return null;
            }
            return fileMD5 + extension;
        }
    }
}
using System.Security.Cryptography;
using API.Interfaces;

namespace API.Services{
    public class BookImageService : IBookImageService
    {
        public Tuple<int,string> UploadImage(IFormFile file)
        {
            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName);
            string[] allowedExtensions = {".jpg", ".png", ".jpeg"};
            if (!allowedExtensions.Contains(extension.ToLower()))
            {
                return Tuple.Create(-1, "Only jpg, png and jpeg files are allowed");
            }
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
                    file.CopyTo(stream);
                }
            }
            catch(Exception e){
                return Tuple.Create(-2, e.Message);
            }
            return Tuple.Create(0, fileMD5+"."+extension);
        }
    }
}
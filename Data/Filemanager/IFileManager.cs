namespace blog.Data.Filemanager;

public interface IFileManager
{
    FileStream ImageStream(string image);
    Task<string> SaveImage(IFormFile image);
}
namespace blog.Data.Filemanager;

public class FileManager : IFileManager
{
    private readonly string _imagePath;

    public FileManager(IConfiguration config)
    {
        _imagePath = config["Path:Images"];
    }

    public FileStream ImageStream(string image)
    {
        return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
    }

    public async Task<string> SaveImage(IFormFile image)
    {
        try
        {
            var savePath = Path.Combine(_imagePath);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var fileType = image.FileName.Substring(image.FileName.LastIndexOf('.'));
            var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{fileType}".Trim();

            await using (var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return fileName;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "Error";
        }
    }
}
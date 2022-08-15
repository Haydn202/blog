namespace blog.ViewModels;

public class ArticleViewModel
{
    public int ArticleId { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public string Description { get; set; } = "";
    public IFormFile Thumbnail { get; set; } = null;
    public string Topics { get; set; } = "";
    public DateTime WrittenOn { get; set; } = DateTime.Now;
    public string Content { get; set; } = "";
    public string? CurrentThumbnail { get; set; }
    public string Tags { get; set; } = "";
}
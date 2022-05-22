namespace blog.Models;

public class Article
{
    public int ArticleId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string ThumbnailUrl { get; set; } =
        "https://upload.wikimedia.org/wikipedia/en/thumb/3/3b/SpongeBob_SquarePants_character.svg/640px-SpongeBob_SquarePants_character.svg.png";
    public string Topics { get; set; }
    public DateTime WrittenOn { get; set; } = DateTime.Now;
    public string Content { get; set; } = "Im an article";
}
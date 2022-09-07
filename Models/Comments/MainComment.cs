namespace blog.Models.Comments;

public class MainComment : Comment
{
    public int ArticleId { get; set; }
    public List<SubComment> SubComments { get; set; }
}
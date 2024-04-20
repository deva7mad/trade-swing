namespace TradeSwing.Domain.Entities;

public class ArticleEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; }  = null!;
    public DateTime CreatedAt { get; set; }
}
namespace LearningEntityFramework.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Message { get; set; }
    public User Author { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public WorkItem WorkItem { get; set; }
    public int WorkItemId { get; set; }


}
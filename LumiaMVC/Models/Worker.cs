namespace LumiaMVC.Models;

public class Worker
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } 
    public string ProfileImage { get; set; } = null!;
    public int WorkTypeId { get; set; }
    public WorkType WorkType { get; set; } = null!;
}

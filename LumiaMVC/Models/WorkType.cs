namespace LumiaMVC.Models;

public class WorkType
{
    public int Id { get; set; }
    public string WorkTypeName { get; set; } = null!;
    public List<Worker> Workers { get; set; }

}

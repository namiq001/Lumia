using LumiaMVC.Models;

namespace LumiaMVC.ViewModels.WorkerVM;

public class CreateWorkerVM
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; } 
    public int WorkTypeId { get; set; }
    public IFormFile? Image { get; set; }
    public List<WorkType>? WorkTypes { get; set; }
}

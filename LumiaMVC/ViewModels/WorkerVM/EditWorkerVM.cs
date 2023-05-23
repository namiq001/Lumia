using LumiaMVC.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LumiaMVC.ViewModels.WorkerVM;

public class EditWorkerVM
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int WorkTypeId { get; set; }
    public string? ProfileImage { get; set; }
    public IFormFile? Image { get; set; }
    public List<WorkType>? WorkTypes { get; set; }
}

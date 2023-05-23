using LumiaMVC.LumiaDataContext;
using LumiaMVC.Models;
using LumiaMVC.ViewModels.WorkerVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumiaMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class WorkerController : Controller
{
    private readonly LumiaDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public WorkerController(LumiaDbContext context,IWebHostEnvironment environment)
    {
        _environment = environment;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Worker> workers = await _context.Workers.Include(x => x.WorkType).ToListAsync();
        return View(workers);
    }
    public async Task<IActionResult> Create()
    {
        CreateWorkerVM createWorker = new CreateWorkerVM()
        {
            WorkTypes = await _context.WorkTypes.ToListAsync(),
        };
        return View(createWorker);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateWorkerVM createWorker)
    { 
        createWorker.WorkTypes = await _context.WorkTypes.ToListAsync();
        if (!ModelState.IsValid) { return NotFound(); }
        string newFileName = Guid.NewGuid().ToString() + createWorker.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "testimonials", newFileName);
        using (FileStream stream = new FileStream(path, FileMode.CreateNew))
        {
            await createWorker.Image.CopyToAsync(stream);
        }
        Worker worker = new Worker()
        {
            Name = createWorker.Name,
            Description = createWorker.Description,
            WorkTypeId = createWorker.WorkTypeId,
        };
        worker.ProfileImage = newFileName;
        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int Id)
    { 
        Worker? worker = await _context.Workers.FindAsync(Id);
        if(worker is null) { return NotFound(); }
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "testimonials", worker.ProfileImage);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int Id)
    {
        Worker? worker = await _context.Workers.FindAsync(Id);
        if (worker is null)
        {
            return NotFound();
        }
        EditWorkerVM editWorker = new EditWorkerVM()
        {
            Name = worker.Name,
            Description = worker.Description,
            WorkTypeId = worker.WorkTypeId,
            WorkTypes = await _context.WorkTypes.ToListAsync(),
            ProfileImage = worker.ProfileImage,
        };
        return View(editWorker);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditWorkerVM editWorker)
    {
        Worker? worker = await _context.Workers.FindAsync(Id);
        if (worker is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            editWorker.WorkTypes = await _context.WorkTypes.ToListAsync();
            return View(editWorker);
        }
        if (editWorker.ProfileImage is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "testimonials", worker.ProfileImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string newFileName = Guid.NewGuid().ToString() + editWorker.Image.FileName;
            string newPath = Path.Combine(_environment.WebRootPath, "assets", "img", "testimonials", newFileName);
            using (FileStream stream = new FileStream(newPath, FileMode.CreateNew))
            {
                await editWorker.Image.CopyToAsync(stream);
            }
            worker.ProfileImage = newFileName;
        }
        worker.Name = editWorker.Name;
        worker.Description = editWorker.Description;
        worker.WorkTypeId = editWorker.WorkTypeId;
        _context.Workers.Update(worker);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

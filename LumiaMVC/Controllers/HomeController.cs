using LumiaMVC.LumiaDataContext;
using LumiaMVC.Models;
using LumiaMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumiaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly LumiaDbContext _context;

        public HomeController(LumiaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Worker> workers = await _context.Workers.Include(x => x.WorkType).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Workers = workers,
            };
            return View(homeVM);
        }

       
    }
}
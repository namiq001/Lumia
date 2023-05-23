using LumiaMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LumiaMVC.LumiaDataContext;

public class LumiaDbContext : IdentityDbContext<AppUser>
{
	public LumiaDbContext(DbContextOptions<LumiaDbContext> options) : base(options)
	{

	}
	public DbSet<Worker> Workers { get; set; }
	public DbSet<WorkType> WorkTypes { get; set; }
}

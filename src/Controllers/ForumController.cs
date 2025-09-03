using System.Threading.Tasks;
using edu_for_you.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YourAppNamespace.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly AppDbContext _context;

        public ForumController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Forum
        public async Task<IActionResult> Index()
        {
            var forums = await _context.Foruns
                                       .Include(f => f.Curso)
                                       .ToListAsync();
            if (forums.Any())
            {
                ViewData["MostrarLinkForum"] = true;
                ViewData["ForumIdCurso"] = forums.First().id_curso;
            }
            return View(forums);
        }

        // GET: /Forum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var forum = await _context.Foruns
                               .Include(f => f.Curso)
                               .Include(f => f.Topicos) 
                               .FirstOrDefaultAsync(f => f.id_curso == id.Value);

            if (forum == null)
                return NotFound();

            return View(forum);
        }
    }
}
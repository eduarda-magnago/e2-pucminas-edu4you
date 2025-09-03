using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using edu_for_you.Models;
using Microsoft.AspNetCore.Authorization;

namespace edu_for_you.Controllers
{
    [Authorize]
    public class MatriculaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MatriculaController> _logger;

        public MatriculaController(AppDbContext context, ILogger<MatriculaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Matricula
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Matriculas.Include(m => m.Curso).Include(m => m.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Matricula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.Curso)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // GET: Matricula/Create
        public IActionResult Create()
        {
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria");
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email");
            return View();
        }

        // POST: Matricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,id_usuario,id_curso,data_matricula")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria", matricula.id_curso);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", matricula.id_usuario);
            return View(matricula);
        }

        // GET: Matricula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria", matricula.id_curso);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", matricula.id_usuario);
            return View(matricula);
        }

        // POST: Matricula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_usuario,id_curso,data_matricula")] Matricula matricula)
        {
            if (id != matricula.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria", matricula.id_curso);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", matricula.id_usuario);
            return View(matricula);
        }

        // GET: Matricula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.Curso)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: Matricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula != null)
            {
                _context.Matriculas.Remove(matricula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Inscrever(int id_curso)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.email == email);

            if (usuario == null)
            {
                _logger.LogInformation("Usuário autenticado: {User}", User.Identity.Name);
                return Unauthorized();
            }
            // Verifica se já está matriculado
            var existeMatricula = await _context.Matriculas
                .AnyAsync(m => m.id_usuario == usuario.id && m.id_curso == id_curso);

            if (existeMatricula)
            {
                TempData["Mensagem"] = "Você já está matriculado neste curso.";
                return RedirectToAction("Index", "Curso"); // ou outra página
            }

            var matricula = new Matricula
            {
                id_usuario = usuario.id,
                id_curso = id_curso,
                data_matricula = DateTime.Now
            };

            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Matrícula realizada com sucesso!";
            return RedirectToAction("Index", "Curso"); // ou para a página do curso
        }

        [Authorize]
        public async Task<IActionResult> MeusCursos()
        {
            var nomeUsuario = User.Identity.Name;

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.nome == nomeUsuario);

            if (usuario == null)
            {
                _logger.LogWarning("Usuário não encontrado: {User}", nomeUsuario);
                return Unauthorized();
            }

            var matriculas = await _context.Matriculas
                .Include(m => m.Curso)
                .Where(m => m.id_usuario == usuario.id)
                .ToListAsync();

            return View(matriculas);
        }


        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.id == id);
        }
    }
}

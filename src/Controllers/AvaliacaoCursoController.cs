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
    public class AvaliacaoCursoController : Controller
    {
        private readonly AppDbContext _context;

        public AvaliacaoCursoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AvaliacaoCurso
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AvaliacaoCursos.Include(a => a.Curso).Include(a => a.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AvaliacaoCurso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacaoCurso = await _context.AvaliacaoCursos
                .Include(a => a.Curso)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (avaliacaoCurso == null)
            {
                return NotFound();
            }

            return View(avaliacaoCurso);
        }

        // GET: AvaliacaoCurso/Create
        public IActionResult Create()
        {
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria");
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email");
            return View();
        }

        // POST: AvaliacaoCurso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,id_usuario,id_curso,nota,comentario,data_avaliacao")] AvaliacaoCurso avaliacaoCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avaliacaoCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria", avaliacaoCurso.id_curso);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", avaliacaoCurso.id_usuario);
            return View(avaliacaoCurso);
        }

        // GET: AvaliacaoCurso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacaoCurso = await _context.AvaliacaoCursos.FindAsync(id);
            if (avaliacaoCurso == null)
            {
                return NotFound();
            }
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria", avaliacaoCurso.id_curso);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", avaliacaoCurso.id_usuario);
            return View(avaliacaoCurso);
        }

        // POST: AvaliacaoCurso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_usuario,id_curso,nota,comentario,data_avaliacao")] AvaliacaoCurso avaliacaoCurso)
        {
            if (id != avaliacaoCurso.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacaoCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoCursoExists(avaliacaoCurso.id))
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
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria", avaliacaoCurso.id_curso);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", avaliacaoCurso.id_usuario);
            return View(avaliacaoCurso);
        }

        // GET: AvaliacaoCurso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacaoCurso = await _context.AvaliacaoCursos
                .Include(a => a.Curso)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (avaliacaoCurso == null)
            {
                return NotFound();
            }

            return View(avaliacaoCurso);
        }

        // POST: AvaliacaoCurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacaoCurso = await _context.AvaliacaoCursos.FindAsync(id);
            if (avaliacaoCurso != null)
            {
                _context.AvaliacaoCursos.Remove(avaliacaoCurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoCursoExists(int id)
        {
            return _context.AvaliacaoCursos.Any(e => e.id == id);
        }
    }
}

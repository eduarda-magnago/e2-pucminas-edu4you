using edu_for_you.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_for_you.Controllers
{
    public class ConteudoController : Controller
    {
        private readonly AppDbContext _context;

        public ConteudoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Conteudo
        public async Task<IActionResult> Index(int? cursoId)
        {
            var conteudos = _context.Conteudos.Include(c => c.Curso).AsQueryable();

            if (cursoId.HasValue)
            {
                conteudos = conteudos.Where(c => c.id_curso == cursoId.Value);
                var curso = await _context.Cursos
                    .Include(c => c.Forum)
                    .FirstOrDefaultAsync(c => c.id == cursoId.Value);
                ViewBag.NomeCurso = curso?.nome ?? "Curso não encontrado";
                ViewBag.CursoId = cursoId.Value; // <== GUARDA para usar nas views
                if (curso?.Forum != null)
                {
                    ViewData["MostrarLinkForum"] = true;
                    ViewData["ForumIdCurso"] = curso.Forum.id_curso;
                }
            }
            return View(await conteudos.ToListAsync());
        }

        // GET: Conteudo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conteudo = await _context.Conteudos
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.id == id);
            if (conteudo == null)
            {
                return NotFound();
            }

            return View(conteudo);
        }

        // GET: Conteudo/Create
        [Authorize(Policy = "Professor")]
        public IActionResult Create(int? cursoId)
        {
            if (cursoId.HasValue)
            {
                var conteudo = new Conteudo { id_curso = cursoId.Value };
                return View(conteudo);
            }

            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "descricao");
            return View(new Conteudo());
        }

        // POST: Conteudo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> Create(Conteudo conteudo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conteudo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { cursoId = conteudo.id_curso });
            }

            if (conteudo.id_curso == 0)
            {
                ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "descricao");
            }

            return View(conteudo);
        }

        // GET: Conteudo/Edit/5
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> Edit(int? id, int? cursoId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conteudo = await _context.Conteudos.FindAsync(id);
            if (conteudo == null)
            {
                return NotFound();
            }
            ViewData["id_curso"] = new SelectList(_context.Cursos.Where(c => c.id == conteudo.id_curso), "id", "descricao", conteudo.id_curso);
            ViewBag.CursoId = conteudo.id_curso;
            return View(conteudo);
        }

        // POST: Conteudo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_curso,titulo,licao,descricao,data_criacao,data_atualizacao,video_curso")] Conteudo conteudo)
        {
            if (id != conteudo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conteudo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConteudoExists(conteudo.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { cursoId = conteudo.id_curso }); // redireciona com cursoId
            }
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "descricao", conteudo.id_curso);
            return View(conteudo);
        }

        // GET: Conteudo/Delete/5
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conteudo = await _context.Conteudos
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.id == id);
            if (conteudo == null)
            {
                return NotFound();
            }

            ViewBag.CursoId = conteudo.id_curso;
            return View(conteudo);
        }

        // POST: Conteudo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conteudo = await _context.Conteudos.FindAsync(id);
            int cursoId = 0;
            if (conteudo != null)
            {
                cursoId = conteudo.id_curso;
                _context.Conteudos.Remove(conteudo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { cursoId }); // redireciona com cursoId
        }

        private bool ConteudoExists(int id)
        {
            return _context.Conteudos.Any(e => e.id == id);
        }
    }
}

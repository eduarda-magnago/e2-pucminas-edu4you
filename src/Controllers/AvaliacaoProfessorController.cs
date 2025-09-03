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
    public class AvaliacaoProfessorController : Controller
    {
        private readonly AppDbContext _context;

        public AvaliacaoProfessorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AvaliacaoProfessor
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AvaliacaoProfessores.Include(a => a.PerfilProfessor).Include(a => a.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AvaliacaoProfessor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacaoProfessor = await _context.AvaliacaoProfessores
                .Include(a => a.PerfilProfessor)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (avaliacaoProfessor == null)
            {
                return NotFound();
            }

            return View(avaliacaoProfessor);
        }

        // GET: AvaliacaoProfessor/Create
        public IActionResult Create()
        {
            ViewData["id_professor"] = new SelectList(_context.PerfilProfessores, "id", "formacao_academica");
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email");
            return View();
        }

        // POST: AvaliacaoProfessor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,id_usuario,id_professor,nota,comentario,data_avaliacao")] AvaliacaoProfessor avaliacaoProfessor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avaliacaoProfessor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_professor"] = new SelectList(_context.PerfilProfessores, "id", "formacao_academica", avaliacaoProfessor.id_professor);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", avaliacaoProfessor.id_usuario);
            return View(avaliacaoProfessor);
        }

        // GET: AvaliacaoProfessor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacaoProfessor = await _context.AvaliacaoProfessores.FindAsync(id);
            if (avaliacaoProfessor == null)
            {
                return NotFound();
            }
            ViewData["id_professor"] = new SelectList(_context.PerfilProfessores, "id", "formacao_academica", avaliacaoProfessor.id_professor);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", avaliacaoProfessor.id_usuario);
            return View(avaliacaoProfessor);
        }

        // POST: AvaliacaoProfessor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_usuario,id_professor,nota,comentario,data_avaliacao")] AvaliacaoProfessor avaliacaoProfessor)
        {
            if (id != avaliacaoProfessor.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacaoProfessor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoProfessorExists(avaliacaoProfessor.id))
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
            ViewData["id_professor"] = new SelectList(_context.PerfilProfessores, "id", "formacao_academica", avaliacaoProfessor.id_professor);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", avaliacaoProfessor.id_usuario);
            return View(avaliacaoProfessor);
        }

        // GET: AvaliacaoProfessor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacaoProfessor = await _context.AvaliacaoProfessores
                .Include(a => a.PerfilProfessor)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (avaliacaoProfessor == null)
            {
                return NotFound();
            }

            return View(avaliacaoProfessor);
        }

        // POST: AvaliacaoProfessor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacaoProfessor = await _context.AvaliacaoProfessores.FindAsync(id);
            if (avaliacaoProfessor != null)
            {
                _context.AvaliacaoProfessores.Remove(avaliacaoProfessor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoProfessorExists(int id)
        {
            return _context.AvaliacaoProfessores.Any(e => e.id == id);
        }
    }
}

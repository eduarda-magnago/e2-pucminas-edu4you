using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using edu_for_you.Models;
using Microsoft.AspNetCore.Authorization;
using edu_for_you.ViewModels;

namespace edu_for_you.Controllers
{
   
    public class PerfilProfessorController : Controller
    {
        private readonly AppDbContext _context;

        public PerfilProfessorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PerfilProfessor
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PerfilProfessores.Include(p => p.Usuario);
            return View(await appDbContext.ToListAsync());
        }


        // GET: PerfilProfessor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilProfessor = await _context.PerfilProfessores
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (perfilProfessor == null)
            {
                return NotFound();
            }

            return View(perfilProfessor);
        }

        // GET: PerfilProfessor/Create
       
        public IActionResult Create(int id_usuario)
        {
            var perfilProfessor = new PerfilProfessor
            {
                id_usuario = id_usuario
            };

            return View();
        }

        // POST: PerfilProfessor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PerfilProfessorUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.usuario.senha = BCrypt.Net.BCrypt.HashPassword(model.usuario.senha);
                model.perfilProfessor.Usuario = model.usuario;
                _context.Add(model.perfilProfessor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: PerfilProfessor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilProfessor = await _context.PerfilProfessores.FindAsync(id);
            if (perfilProfessor == null)
            {
                return NotFound();
            }
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", perfilProfessor.id_usuario);
            return View(perfilProfessor);
        }

        // POST: PerfilProfessor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_usuario,nome,sobrenome,senha,dataNascimento,genero,areaInteresse,nivelEnsino,biografia,email_contato,telefone_contato,formacao_academica,experiencia_profissional,habilidades,idiomas,linkedin,avaliacao_media")] PerfilProfessor perfilProfessor)
        {
            if (id != perfilProfessor.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilProfessor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilProfessorExists(perfilProfessor.id))
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
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", perfilProfessor.id_usuario);
            return View(perfilProfessor);
        }

        // GET: PerfilProfessor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilProfessor = await _context.PerfilProfessores
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (perfilProfessor == null)
            {
                return NotFound();
            }

            return View(perfilProfessor);
        }

        // POST: PerfilProfessor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perfilProfessor = await _context.PerfilProfessores.FindAsync(id);
            if (perfilProfessor != null)
            {
                _context.PerfilProfessores.Remove(perfilProfessor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilProfessorExists(int id)
        {
            return _context.PerfilProfessores.Any(e => e.id == id);
        }
    }
}

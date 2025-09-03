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
    public class PostagemController : Controller
    {
        private readonly AppDbContext _context;

        public PostagemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Postagem
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Postagens.Include(p => p.Curso).Include(p => p.PostagemPai).Include(p => p.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Postagem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postagem = await _context.Postagens
                .Include(p => p.Curso)
                .Include(p => p.PostagemPai)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (postagem == null)
            {
                return NotFound();
            }

            return View(postagem);
        }

        // GET: Postagem/Create
        public IActionResult Create()
        {
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria");
            ViewData["postagem_pai_id"] = new SelectList(_context.Postagens, "id", "conteudo");
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email");
            return View();
        }

        // POST: Postagem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("postagem_pai_id,id_curso,titulo,conteudo")] Postagem postagem)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.nome == User.Identity.Name);
            if (usuario == null)
                return Unauthorized();

            postagem.id_usuario = usuario.id;
            postagem.data_criacao = DateTime.UtcNow;
            postagem.data_atualizacao = DateTime.UtcNow;

            _context.Add(postagem);
            await _context.SaveChangesAsync();

            // Redireciona de volta ao Topico baseado no título da postagem pai
            var postagemPai = await _context.Postagens
                .FirstOrDefaultAsync(p => p.id == postagem.postagem_pai_id);
            if (postagemPai != null)
            {
                var topico = await _context.Topicos
                    .FirstOrDefaultAsync(t => t.Titulo == postagemPai.titulo);
                if (topico != null)
                {
                    return RedirectToAction("Details", "Topico", new { id = topico.Id });
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Postagem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postagem = await _context.Postagens.FindAsync(id);
            if (postagem == null)
            {
                return NotFound();
            }
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria", postagem.id_curso);
            ViewData["postagem_pai_id"] = new SelectList(_context.Postagens, "id", "conteudo", postagem.postagem_pai_id);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", postagem.id_usuario);
            return View(postagem);
        }

        // POST: Postagem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,id_curso,id_usuario,postagem_pai_id,titulo,conteudo,data_criacao,data_atualizacao")] Postagem postagem)
        {
            if (id != postagem.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostagemExists(postagem.id))
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
            ViewData["id_curso"] = new SelectList(_context.Cursos, "id", "categoria", postagem.id_curso);
            ViewData["postagem_pai_id"] = new SelectList(_context.Postagens, "id", "conteudo", postagem.postagem_pai_id);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id", "email", postagem.id_usuario);
            return View(postagem);
        }

        // GET: Postagem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postagem = await _context.Postagens
                .Include(p => p.Curso)
                .Include(p => p.PostagemPai)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (postagem == null)
            {
                return NotFound();
            }

            return View(postagem);
        }

        // POST: Postagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postagem = await _context.Postagens.FindAsync(id);
            if (postagem != null)
            {
                _context.Postagens.Remove(postagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostagemExists(int id)
        {
            return _context.Postagens.Any(e => e.id == id);
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using edu_for_you.Models;

namespace edu_for_you.Controllers
{
    [Authorize]
    public class TopicoController : Controller
    {
        private readonly AppDbContext _context;
        public TopicoController(AppDbContext context)
            => _context = context;

        [HttpGet]
        public async Task<IActionResult> Create(int forumId)
        {
            // Verify the forum actually exists
            var forum = await _context.Foruns
                                      .FirstOrDefaultAsync(f => f.Id == forumId);
            if (forum == null) return NotFound();

            // Pass the forumId into the view via ViewBag
            ViewBag.ForumId = forumId;
            ViewBag.ForumTitulo = forum.Titulo;
            return View();
        }

        // POST: /Topico/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int forumId,
            [Bind("Titulo,Conteudo")] Topico topico)
        {
            var forum = await _context.Foruns.FindAsync(forumId);
            if (forum == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.ForumId = forumId;
                ViewBag.ForumTitulo = forum.Titulo;
                return View(topico);
            }

            topico.id_forum = forumId;
            topico.DataCriacao = DateTime.UtcNow;
            topico.Autor = User.Identity.Name;

            _context.Topicos.Add(topico);
            await _context.SaveChangesAsync();

            // Cria a postagem principal associada ao tópico
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.nome == User.Identity.Name);
            if (usuario != null)
            {
                var postagemPrincipal = new Postagem
                {
                    titulo = topico.Titulo,
                    conteudo = topico.Conteudo,
                    id_usuario = usuario.id,
                    id_curso = forum.id_curso,
                    data_criacao = DateTime.UtcNow,
                    data_atualizacao = DateTime.UtcNow,
                    postagem_pai_id = null // raiz
                };

                _context.Postagens.Add(postagemPrincipal);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Forum", new { id = forumId });
        }

        // GET: /Topico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return NotFound();

            var topico = await _context.Topicos
                .Include(t => t.Forum)
                .ThenInclude(f => f.Curso)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (topico == null) return NotFound();

            // Busca a postagem principal (com titulo igual ao do tópico e postagem_pai_id == null)
            var postagemPrincipal = await _context.Postagens
                .Include(p => p.Usuario)
                .Where(p => p.titulo == topico.Titulo && p.postagem_pai_id == null)
                .OrderBy(p => p.data_criacao)
                .FirstOrDefaultAsync();

            // Busca as respostas ligadas à postagem principal
            List<Postagem> respostas = new();
            if (postagemPrincipal != null)
            {
                respostas = await _context.Postagens
                    .Include(p => p.Usuario)
                    .Where(p => p.postagem_pai_id == postagemPrincipal.id)
                    .OrderBy(p => p.data_criacao)
                    .ToListAsync();
            }

            ViewBag.PostagemPrincipal = postagemPrincipal;
            ViewBag.Respostas = respostas;

            return View(topico);
        }
    }
}

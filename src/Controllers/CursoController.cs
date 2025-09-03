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
using Microsoft.IdentityModel.Tokens;

namespace edu_for_you.Controllers
{
    [Authorize]
    public class CursoController : Controller
    {
        private readonly AppDbContext _context;

        public CursoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Curso
        public async Task<IActionResult> Index()
        {
            // Agrupar os 10 cursos criados mais recentemente de cada categoria
            var cursosRecentes = await _context.Cursos
                .Include(c => c.PerfilProfessor)
                .ThenInclude(p => p.Usuario)
                .OrderBy(c => c.categoria)
                .ThenByDescending(c => c.data_criacao)
                .ToListAsync();

            var cursosByCategory = cursosRecentes
                .GroupBy(c => c.categoria)
                .ToDictionary(g => g.Key, g => g.Take(20).AsEnumerable());

            return View(cursosByCategory);
        }

        public async Task<IActionResult> Search(string query)
        {
            query = query?.Trim();

            var resultados =
                (query == null || query.IsNullOrEmpty()) ? [] :
                    await _context.Cursos
                        .Include(c => c.PerfilProfessor)
                        .ThenInclude(p => p.Usuario)
                        .Where(c => c.nome.ToUpper().Contains(query.ToUpper()))
                        .ToListAsync();

            ViewBag.query = query;

            return View(resultados);
        }

        // GET: Curso/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            bool jaMatriculado = false;

            if (User.Identity.IsAuthenticated)
            {
                var nomeUsuario = User.Identity.Name;
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.nome == nomeUsuario);

                if (usuario != null)
                {
                    jaMatriculado = await _context.Matriculas
                        .AnyAsync(m => m.id_usuario == usuario.id && m.id_curso == curso.id);
                }
            }

            ViewData["JaMatriculado"] = jaMatriculado;
            ViewData["MostrarLinkForum"] = true;
            ViewData["ForumIdCurso"] = curso.id;
            ViewData["NumeroDeLicoes"] = await _context.Conteudos.Where(c => c.id_curso == id).CountAsync();
            ViewData["EstudantesInscritos"] = await _context.Matriculas.Where(m => m.id_curso == id).CountAsync();


            return View(curso);
        }

        // GET: Curso/Create
        [Authorize(Policy = "Professor")]
        public IActionResult Create()
        {
            return View(new Curso());
        }

        // POST: Curso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> Create(
            [Bind("nome,descricao,categoria,carga_horaria,capa_curso")] Curso curso)
        {
            if (!ModelState.IsValid)
                return View(curso);

            // 1) Get the current professor profile
            var perfilProfessor = await User.GetPerfilProfessor(_context);
            if (perfilProfessor == null)
            {
                ModelState.AddModelError("", "Perfil do professor não encontrado.");
                return View(curso);
            }

            // 2) Populate curso and its Forum in one go
            curso.data_criacao = DateTime.UtcNow;
            curso.id_professor = perfilProfessor.id;
            curso.Forum = new Forum
            {
                Titulo = $"Fórum de {curso.nome}",
                Descricao = $"Discussões sobre o curso {curso.nome}",
            };

            // 3) Save both entities atomically
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Curso/Edit/5
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            
            if (curso.id_professor != User.GetPerfilProfessorId())
            {
                return NotFound();
            }

            return View(new CursoEditViewModel(curso));
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> Edit(int id, CursoEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var curso = await _context.Cursos.FindAsync(id);

            if (curso.id_professor != User.GetPerfilProfessorId())
            {
                return NotFound();
            }

            model.WriteTo(ref curso);

            _context.Update(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Curso/Delete/5
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            if (curso.id_professor != User.GetPerfilProfessorId())
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Curso/Professor/5
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> MeusCursos()
        {
            var perfilProfessor = await User.GetPerfilProfessor(_context);
            if (perfilProfessor == null)
            {
                return Unauthorized();
            }

            var cursos = await _context.Cursos
                .Where(c => c.id_professor == perfilProfessor.id)
                .OrderByDescending(c => c.data_criacao)
                .ToListAsync();

            return View(cursos); //
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                if (curso.id_professor != User.GetPerfilProfessorId())
                {
                    return NotFound();
                }

                _context.Cursos.Remove(curso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Relaciona os cursos com o Dashboard
        [Authorize]
        public async Task<IActionResult> DashboardAluno()
        {
            var nomeUsuario = User.Identity.Name;

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.nome == nomeUsuario);
            if (usuario == null)
                return Unauthorized();

            var cursosMatriculados = await _context.Matriculas
                .Where(m => m.id_usuario == usuario.id)
                .Include(m => m.Curso)
                    .ThenInclude(c => c.Forum)
                .Include(m => m.Curso.PerfilProfessor)
                    .ThenInclude(p => p.Usuario)
                .ToListAsync();

            // Extrai cursos e fóruns
            var cursos = cursosMatriculados.Select(m => m.Curso).ToList();
            var foruns = cursosMatriculados
                .Select(m => m.Curso.Forum)
                .Where(f => f != null)
                .ToList();

            ViewData["ForunsInscritos"] = foruns;
            ViewData["CursosEmProgresso"] = cursos.Count;

            var cursoMaisRecente = cursosMatriculados
                .OrderByDescending(m => m.Curso.data_criacao)
                .Select(m => m.Curso)
                .FirstOrDefault();

            ViewData["CursoMaisRecente"] = cursoMaisRecente;


            return View("~/Views/Usuario/Index.cshtml", cursos);
        }


        [Authorize(Policy = "Professor")]
        public async Task<IActionResult> DashboardProfessor()
        {
            var perfilProfessor = await User.GetPerfilProfessor(_context);
            if (perfilProfessor == null)
            {
                return Unauthorized();
            }

            var cursosDoProfessor = await _context.Cursos
                .Where(c => c.id_professor == perfilProfessor.id)
                .Include(c => c.Forum)
                .Include(c => c.PerfilProfessor)
                    .ThenInclude(p => p.Usuario)
                .OrderByDescending(c => c.data_criacao)
                .ToListAsync();

            // Extrai fóruns ativos dos cursos criados
            var foruns = cursosDoProfessor
                .Where(c => c.Forum != null)
                .Select(c => c.Forum)
                .ToList();

            ViewData["ForunsAtivos"] = foruns;
            ViewData["CursosEmProgresso"] = cursosDoProfessor.Count;
            ViewData["CursosCompletos"] = cursosDoProfessor.Count;

            // Envia cursos para a View: Views/PerfilProfessor/Index.cshtml
            return View("~/Views/PerfilProfessor/Index.cshtml", cursosDoProfessor);
        }

        // Relaciona os cursos com o Dashboard

    }
}

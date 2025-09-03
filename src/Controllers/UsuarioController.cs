using System.Security.Claims;
using System.Threading.Tasks;
using edu_for_you.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace edu_for_you.Controllers
{
    [Authorize]
    public class UsuarioController : Controller

    {
        private AppDbContext _context;
        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dados = await _context.Usuarios.ToListAsync();
            return View(dados);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() 
        {
            return View();
        }

        //LOGIN
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            var dados = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.email == usuario.email);

            if (dados == null || !BCrypt.Net.BCrypt.Verify(usuario.senha, dados.senha))
            {
                ViewBag.Message = "Usuário e/ou senha inválidos!";
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, dados.id.ToString()),
        new Claim(ClaimTypes.Name, dados.nome),
        new Claim(ClaimTypes.Email, dados.email),
        new Claim("foto_perfil", dados.foto_perfil ?? "/images/fotodousuario.PNG") // <-- Adicionado aqui
    };

            // Verifica se é professor
            var perfilProfessor = await _context.PerfilProfessores
                .FirstOrDefaultAsync(pp => pp.id_usuario == dados.id);

            if (perfilProfessor != null)
            {
                claims.Add(new Claim(
                    Usuario.ClaimType.PerfilProfessorId,
                    perfilProfessor.id.ToString()
                ));
                claims.Add(new Claim("IsProfessor", "true"));
            }
            else
            {
                claims.Add(new Claim("IsProfessor", "false"));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redireciona conforme o perfil
            if (perfilProfessor != null)
            {
                return RedirectToAction("DashboardProfessor", "Curso");
            }
            else
            {
                return RedirectToAction("DashboardAluno", "Curso");
            }
        }
        // FIM LOGIN

        //LOGOUT 
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Usuario");
        }

        //LOGOUT FIM

        //CRUDE
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.senha = BCrypt.Net.BCrypt.HashPassword(usuario.senha);
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Usuario");
            }

            return View(usuario);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dados = await _context.Usuarios.FindAsync(id);

            if (dados == null)
            {
                return NotFound();
            }

            return View(dados);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Usuario usuario, IFormFile foto_perfil)
        {
            if (id != usuario.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExistente = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.id == id);

                    if (usuarioExistente == null)
                    {
                        return NotFound();
                    }

                    // Se não alterar a senha, mantém a antiga
                    if (string.IsNullOrWhiteSpace(usuario.senha))
                    {
                        usuario.senha = usuarioExistente.senha;
                    }
                    else
                    {
                        usuario.senha = BCrypt.Net.BCrypt.HashPassword(usuario.senha);
                    }

                    // Lógica para salvar a nova foto
                    if (foto_perfil != null && foto_perfil.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "usuarios");

                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(foto_perfil.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await foto_perfil.CopyToAsync(stream);
                        }

                        usuario.foto_perfil = $"/img/usuarios/{fileName}";
                    }
                    else
                    {
                        // Se nenhuma nova foto for enviada, mantém a antiga
                        usuario.foto_perfil = usuarioExistente.foto_perfil;
                    }

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();

                    // Atualiza os claims para refletir a nova foto
                    var identity = (ClaimsIdentity)User.Identity;
                    var fotoClaim = identity.FindFirst("foto_perfil");
                    if (fotoClaim != null)
                        identity.RemoveClaim(fotoClaim);

                    identity.AddClaim(new Claim("foto_perfil", usuario.foto_perfil ?? "/images/fotodousuario.PNG"));

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity)
                    );

                    return RedirectToAction("Details", new { id = usuario.id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }

            return View(usuario);
        }





        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dados = await _context.Usuarios.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(dados);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dados = await _context.Usuarios.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(dados);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dados = await _context.Usuarios.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            foreach (var perfilProfessor in await _context.PerfilProfessores.Where(p => p.id_usuario == id).ToListAsync())
            {
                _context.PerfilProfessores.Remove(perfilProfessor);
            }

            _context.Usuarios.Remove(dados);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

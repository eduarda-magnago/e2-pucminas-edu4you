using edu_for_you.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_for_you.Models
{
    public class AppDbContext : DbContext
    {
        private readonly IHostEnvironment _env;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHostEnvironment env) : base(options)
        {
            _env = env;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PerfilProfessor> PerfilProfessores { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<AvaliacaoCurso> AvaliacaoCursos { get; set; }
        public DbSet<AvaliacaoProfessor> AvaliacaoProfessores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Forum> Foruns { get; set; }
        public DbSet<Topico> Topicos { get; set; }


        // Configura os relacionamentos para evitar múltiplos caminhos de cascata
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PerfilProfessor -> Usuario (many-to-one)
            modelBuilder.Entity<PerfilProfessor>()
                .HasOne(pp => pp.Usuario)
                .WithMany()
                .HasForeignKey(pp => pp.id_usuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Curso -> Forum (one-to-one)
            modelBuilder.Entity<Forum>()
                .HasOne(f => f.Curso)
                .WithOne(c => c.Forum)
                .HasForeignKey<Forum>(f => f.id_curso)
                .OnDelete(DeleteBehavior.Cascade);

            // Forum -> Topico (one-to-many)
            modelBuilder.Entity<Topico>()
                .HasOne(t => t.Forum)
                .WithMany(f => f.Topicos)
                .HasForeignKey(t => t.id_forum)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public static async Task SeedTestData(DbContext context)
        {
            var addTestAluno = async (Usuario aluno) =>
            {
                if (!await context.Set<Usuario>().AnyAsync(u => u.email == aluno.email))
                {
                    context.Set<Usuario>().Add(aluno);
                    return true;
                }
                return false;
            };

            var addTestProfessor = async (Usuario usuario, PerfilProfessor perfilProfessor) =>
            {
                if (await addTestAluno(usuario))
                {
                    perfilProfessor.Usuario = usuario;
                    context.Set<PerfilProfessor>().Add(perfilProfessor);
                }
            };

            await addTestAluno(new Usuario
            {
                nome = "Aluno1",
                sobrenome = "da Silva",
                email = "aluno1@gmail.com",
                senha = BCrypt.Net.BCrypt.HashPassword("123"),
                dataNascimento = DateTime.Now,
                genero = "Masculino",
                nivelEnsino = "Médio",
                areaInteresse = "Programação",
            });
            await addTestAluno(new Usuario
            {
                nome = "Aluno2",
                sobrenome = "da Silva",
                email = "aluno2@gmail.com",
                senha = BCrypt.Net.BCrypt.HashPassword("123"),
                dataNascimento = DateTime.Now,
                genero = "Feminino",
                nivelEnsino = "Superior",
                areaInteresse = "Idiomas",
            });

            await addTestProfessor(new Usuario
            {
                nome = "Professor1",
                sobrenome = "da Silva",
                email = "professor1@gmail.com",
                senha = BCrypt.Net.BCrypt.HashPassword("123"),
                dataNascimento = DateTime.Now,
                genero = "Masculino",
                nivelEnsino = "Médio",
                areaInteresse = "Programação",
            }, new PerfilProfessor
            {
                biografia = "Bla bla bla",
                email_contato = "professor1@gmail.com",
                areaAtuacao = "Programação",
                telefone_contato = "(11) 99123-4567",
                formacao_academica = "",
                experiencia_profissional = "1 ano",
                habilidades = "Python, Java",
                idiomas = "Portugues e Ingles",
                linkedin = "linkedin.com/professor1",
                avaliacao_media = 4.5,
            });
            await addTestProfessor(new Usuario
            {
                nome = "Professor2",
                sobrenome = "da Silva",
                email = "professor2@gmail.com",
                senha = BCrypt.Net.BCrypt.HashPassword("123"),
                dataNascimento = DateTime.Now,
                genero = "Feminino",
                nivelEnsino = "Superior",
                areaInteresse = "Idiomas",
            }, new PerfilProfessor
            {
                biografia = "Bla bla bla",
                email_contato = "professor2@gmail.com",
                areaAtuacao = "Idiomas",
                telefone_contato = "(11) 99321-7654",
                formacao_academica = "",
                experiencia_profissional = "+ 5 anos",
                habilidades = "Varias",
                idiomas = "Todos",
                linkedin = "linkedin.com/professor2",
                avaliacao_media = 5.0,
            });

            await context.SaveChangesAsync();
        }
    }
}



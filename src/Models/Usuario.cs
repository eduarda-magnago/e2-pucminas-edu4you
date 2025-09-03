using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace edu_for_you.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome do usuário.")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o sobrenome do usuário.")]
        [Display(Name = "Sobrenome")]
        public string sobrenome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o email.")]
        [Display(Name = "E-mail")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        [Required(ErrorMessage = "Obrigatório informar data de nascimento.")]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime dataNascimento { get; set; }

        [Display(Name = "Gênero")]
        public string genero { get; set; }

        [Required(ErrorMessage = "Obrigatório selecionar nível de ensino.")]
        [Display(Name = "Nível de ensino")]
        public string nivelEnsino { get; set; }

        [Display(Name = "Área de interesse")]
        public string areaInteresse { get; set; }

        [Display(Name = "Foto")]
        public string foto_perfil { get; set; }

        public string Sobre { get; set; }

        public string Certificacoes { get; set; }


        public async Task<PerfilProfessor> GetPerfilProfessor(AppDbContext context)
        {
            return await context.PerfilProfessores.FirstOrDefaultAsync(pp => pp.id_usuario == id);
        }

        public static class ClaimType
        {
            public const string PerfilProfessorId = "PerfilProfessorId";
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static int? GetPerfilProfessorId(this ClaimsPrincipal user)
        {
            var id = user?.FindFirst(Usuario.ClaimType.PerfilProfessorId)?.Value;
            return id == null ? null : int.Parse(id);
        }

        public static async Task<PerfilProfessor> GetPerfilProfessor(this ClaimsPrincipal user, AppDbContext context)
        {
            var id = user.GetPerfilProfessorId();
            return id == null ? null : await context.PerfilProfessores.FindAsync(id);
        }
    }
}

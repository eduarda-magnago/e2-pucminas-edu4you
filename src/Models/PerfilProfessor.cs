using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace edu_for_you.Models
{
    [Table("PerfilProfessor")]
    public class PerfilProfessor
    {
        [Key]
        public int id { get; set; }

        public int id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public Usuario Usuario { get; set; }

        [Display(Name = "Biografia")]
        public string biografia { get; set; }

        [Display(Name = "E-mail de contato")]
        public string email_contato { get; set; }

        [Display(Name = "Área de atuação")]
        public string areaAtuacao { get; set; }

        [Display(Name = "Telefone")]
        public string telefone_contato { get; set; }

        [Display(Name = "Formação acadêmica")]
        public string formacao_academica { get; set; }

        [Display(Name = "Anos de experiência")]
        public string experiencia_profissional { get; set; }

        [Display(Name = "Habilidades")]
        public string habilidades { get; set; }

        [Display(Name = "Linguas faladas")]
        public string idiomas { get; set; }

        [Display(Name = "Perfil do LinkedIn")]
        public string linkedin { get; set; }

        [Display(Name = "Avaliação média")]
        public double avaliacao_media { get; set; }
    }
}

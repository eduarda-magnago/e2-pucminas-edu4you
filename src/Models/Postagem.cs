using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace edu_for_you.Models
{
    [Table("Postagem")]
    public class Postagem
    {
        [Key]
        public int id { get; set; }


        public int id_curso { get; set; }
        [ForeignKey("id_curso")]
        public Curso Curso { get; set; }


        public int id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public Usuario Usuario { get; set; }


        public int? postagem_pai_id { get; set; }


        [Required(ErrorMessage = "Obrigatório informar um título.")]
        public string titulo { get; set; }


        [Required(ErrorMessage = "Obrigatório informar um conteúdo.")]
        public string conteudo { get; set; }


        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        public DateTime data_criacao { get; set; }


        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        public DateTime data_atualizacao { get; set; }


        [ForeignKey("postagem_pai_id")]
        public Postagem PostagemPai { get; set; }
    }
}

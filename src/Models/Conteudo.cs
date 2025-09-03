using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace edu_for_you.Models
{
    [Table("Conteudo")]
    public class Conteudo
    {
        [Key]
        public int id { get; set; }


        public int id_curso { get; set; }
        [ForeignKey("id_curso")]
        public Curso Curso { get; set; }


        [Required(ErrorMessage = "Obrigatório informar um título.")]
        public string titulo { get; set; }


        [Required(ErrorMessage = "Obrigatório informar um conteúdo.")]
        public string licao { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a descrição do curso.")]
        public string descricao { get; set; }


        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        [DataType(DataType.Date)]
        public DateTime data_criacao { get; set; }


        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        [DataType(DataType.Date)]
        public DateTime data_atualizacao { get; set; }

        [Url]
        public string video_curso { get; set; }
    }
}

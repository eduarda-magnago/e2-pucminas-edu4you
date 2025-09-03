using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace edu_for_you.Models
{
    [Table("Matricula")]
    public class Matricula
    {
        [Key]
        public int id { get; set; }

        
        public int id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public Usuario Usuario { get; set; }


        public int id_curso { get; set; }
        [ForeignKey("id_curso")]
        public Curso Curso { get; set; }


        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        public DateTime data_matricula { get; set; }
    }
}

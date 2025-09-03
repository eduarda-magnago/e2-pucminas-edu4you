using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace edu_for_you.Models
{
    [Table("Curso")]
    public class Curso
    {
        [Key]
        public int id { get; set; }


        public int id_professor { get; set; }
        [ForeignKey("id_professor")]
        [Display(Name = "Professor")]
        public PerfilProfessor PerfilProfessor { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome do curso.")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a descrição do curso.")]
        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a categoria do curso.")]
        [Display(Name = "Categoria")]
        public Categoria categoria { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a carga horária do curso.")]
        [Display(Name = "Carga Horária")]
        public int carga_horaria { get; set; }

        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        [Display(Name = "Data de Criação")]
        public DateTime data_criacao { get; set; }

        [Url]
        public string capa_curso { get; set; }
        public Forum Forum { get; set; }


    }

    public enum Categoria
    {
        [Display(Name = "Educação e Ensino")]
        EducacaoEnsino,
        [Display(Name = "Negócios e Empreendedorismo")]
        NegociosEmpreendedorismo,
        [Display(Name = "Tecnologia e Programação")]
        TecnologiaProgramacao,
        [Display(Name = "Marketing e Comunicação")]
        MarketingComunicacao,
        [Display(Name = "Design e Multimídia")]
        DesignMultimedia,
        [Display(Name = "Desenvolvimento Pessoal e Carreira")]
        DesenvolvimentoPessoalCarreira,
        [Display(Name = "Idiomas")]
        Idiomas,
        [Display(Name = "Saúde e Bem-Estar")]
        SaudeBemEstar,
        [Display(Name = "Arte e Música")]
        ArteMusica,
        [Display(Name = "Direito e Legislação")]
        DireitoLegislacao,
        [Display(Name = "Ciência e Engenharia")]
        CienciaEngenharia,
        [Display(Name = "Finanças e Economia")]
        FinancasEconomia,
        [Display(Name = "Outros")]
        Outros,
    }
}

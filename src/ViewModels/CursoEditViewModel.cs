using edu_for_you.Models;

namespace edu_for_you.ViewModels
{
    public class CursoEditViewModel
    {
        public CursoEditViewModel() { }

        public CursoEditViewModel(Curso curso)
        {
            nome = curso.nome;
            descricao = curso.descricao;
            categoria = curso.categoria;
            carga_horaria = curso.carga_horaria;
            capa_curso = curso.capa_curso;
        }

        public string nome { get; set; }
        public string descricao { get; set; }
        public Categoria categoria { get; set; }
        public int carga_horaria { get; set; }
        public string capa_curso { get; set; }

        public void WriteTo(ref Curso curso)
        {
            curso.nome = nome;
            curso.descricao = descricao;
            curso.categoria = categoria;
            curso.carga_horaria = carga_horaria;
            capa_curso = curso.capa_curso;
        }
    }
}

namespace edu_for_you.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int id_curso { get; set; }
        public Curso Curso { get; set; }
        public List<Topico> Topicos { get; set; } = new();

    }
}

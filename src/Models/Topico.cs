namespace edu_for_you.Models
{
    public class Topico
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public int id_forum { get; set; }
        public Forum Forum { get; set; }
        public string Autor { get; set; }
    }
}

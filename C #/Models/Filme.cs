namespace FilmesAPI.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int Ano { get; set; }
        public double Nota { get; set; }

        public List<Comentario>? Comentarios { get; set; }
    }
}

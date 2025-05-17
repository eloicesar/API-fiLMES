namespace FilmesAPI.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;

        public int FilmeId { get; set; }
        public Filme? Filme { get; set; }
    }
}

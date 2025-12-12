namespace GerenciadorDeTorneios.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public List<Jogadores> Jogadores { get; set; } = new();
    }
}
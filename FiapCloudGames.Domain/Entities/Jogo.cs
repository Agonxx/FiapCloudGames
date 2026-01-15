namespace FiapCloudGames.Domain.Entities
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
    }
}

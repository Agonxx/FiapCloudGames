namespace FiapCloudGames.Domain.DTOs
{
    public class JogoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeComprador { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public decimal PrecoPago { get; set; }
        public DateTime AdquiridoEm { get; set; } = DateTime.UtcNow;
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
    }
}

namespace FiapCloudGames.Domain.Entities
{
    public class UsuarioJogo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int JogoId { get; set; }
        public DateTime AdiquiridoEm { get; set; } = DateTime.UtcNow;
    }
}

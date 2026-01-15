using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Domain.DTOs
{
    public class InfoToken
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ETipoUsuario Nivel { get; set; } = ETipoUsuario.Usuario;
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
    }
}

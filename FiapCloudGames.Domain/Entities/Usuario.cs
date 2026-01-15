using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public ETipoUsuario Nivel { get; set; } = ETipoUsuario.Usuario;
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
    }

    public class UsuarioApi
    {
        public const string Autenticar = "Autenticar";
        public const string Cadastrar = "Cadastrar";
    }

    public class UsuarioClaims
    {
        public const string Id = nameof(Usuario.Id);
        public const string Nome = nameof(Usuario.Nome);
        public const string Email = nameof(Usuario.Email);
        public const string Nivel = nameof(Usuario.Nivel);
        public const string CadastradoEm = nameof(Usuario.CadastradoEm);
    }
}

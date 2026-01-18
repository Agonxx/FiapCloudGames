using FiapCloudGames.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Domain.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(300)]
        public string SenhaHash { get; set; } = string.Empty;
        [Required]
        public ETipoUsuario Nivel { get; set; } = ETipoUsuario.Usuario;
        [Required]
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
        [Required]
        public bool Ativo { get; set; } = true;
    }

    public class UsuarioApi
    {
        public const string Auth = nameof(Auth);
        public const string Create = nameof(Create);
        public const string Update = nameof(Update);
        public const string DeleteById = nameof(DeleteById);
        public const string GetAll = nameof(GetAll);
        public const string GetById = nameof(GetById);
        public const string GetMe = nameof(GetMe);
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

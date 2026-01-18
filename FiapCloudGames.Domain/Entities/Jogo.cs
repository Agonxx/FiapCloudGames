using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiapCloudGames.Domain.Entities
{
    [Index(nameof(Titulo))]
    public class Jogo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Descricao { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        [Required]
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
    }

    public class JogoApi
    {
        public const string Create = nameof(Create);
        public const string Update = nameof(Update);
        public const string DeleteById = nameof(DeleteById);
        public const string GetById = nameof(GetById);
        public const string GetAll = nameof(GetAll);
    }
}

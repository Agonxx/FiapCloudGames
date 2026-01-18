using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiapCloudGames.Domain.Entities
{
    [Index(nameof(UsuarioId), nameof(JogoId), IsUnique = true)]
    public class ItemBiblioteca
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int JogoId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoPago { get; set; }
        [Required]
        public DateTime AdiquiridoEm { get; set; } = DateTime.UtcNow;
    }

    public class ItemBibliotecaApi
    {
        public const string GetUserGamesById = nameof(GetUserGamesById);
        public const string GetMyGames = nameof(GetMyGames);
        public const string BuyGame = nameof(BuyGame);
    }
}

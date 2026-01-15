namespace FiapCloudGames.Domain.DTOs
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiracaoHoras { get; set; }
    }
}

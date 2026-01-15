using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Enums;
using FiapCloudGames.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiapCloudGames.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;

        public TokenService(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GerarToken(int id, string nome, string email, ETipoUsuario nivel, DateTime cadastradoEm)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_settings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(nameof(InfoToken.Id), id.ToString()),
                new Claim(nameof(InfoToken.Nome), nome),
                new Claim(nameof(InfoToken.Email), email),
                new Claim(nameof(InfoToken.Nivel), nivel.ToString()),
                new Claim(nameof(InfoToken.CadastradoEm), cadastradoEm.ToString("o")),
            }),
                Expires = DateTime.UtcNow.AddHours(_settings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GerarToken(int id, string nome, string email, ETipoUsuario nivel, DateTime cadastradoEm);
    }
}

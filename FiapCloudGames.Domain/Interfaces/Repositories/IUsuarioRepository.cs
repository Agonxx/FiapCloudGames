using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<string> Autenticar(string email, string senha);
        Task<bool> Cadastrar(Usuario usuarioObj);
    }
}

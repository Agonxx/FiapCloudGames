using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByEmailAndPassword(string email, string senha);
        Task<Usuario> GetById(int id);
        Task<Usuario> GetMe();
        Task<bool> DeleteById(int id);
        Task<List<Usuario>> GetAll();
        Task<bool> Create(Usuario usuarioObj);
        Task<bool> Update(Usuario usuarioObj);
        Task<bool> EmailExists(string email, int excludeId = 0);
    }
}

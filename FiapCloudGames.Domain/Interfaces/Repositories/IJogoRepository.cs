using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Domain.Interfaces.Repositories
{
    public interface IJogoRepository
    {
        Task<Jogo> GetById(int id);
        Task<bool> DeleteById(int id);
        Task<List<Jogo>> GetAll();
        Task<bool> Create(Jogo jogoObj);
        Task<bool> Update(Jogo jogoObj);
    }
}

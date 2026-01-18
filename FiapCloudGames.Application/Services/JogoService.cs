using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;

namespace FiapCloudGames.Application.Services
{
    public class JogoService
    {
        public readonly IJogoRepository _repo;
        public JogoService(IJogoRepository repo)
        {
            _repo = repo;
        }

        public async Task<Jogo> GetByIdAsync(int id)
        {
            var user = await _repo.GetById(id);
            return user;
        }

        public async Task<List<Jogo>> GetAllAsync()
        {
            var users = await _repo.GetAll();
            return users;
        }

        public async Task<bool> CreateAsync(Jogo jogoObj)
        {
            var ret = await _repo.Create(jogoObj);
            return ret;
        }

        public async Task<bool> UpdateAsync(Jogo jogoObj)
        {
            var ret = await _repo.Update(jogoObj);
            return ret;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var ret = await _repo.DeleteById(id);
            return ret;
        }
    }
}

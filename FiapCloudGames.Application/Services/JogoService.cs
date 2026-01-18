using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Extensions;
using FiapCloudGames.Domain.Interfaces.Repositories;

namespace FiapCloudGames.Application.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repo;

        public JogoService(IJogoRepository repo)
        {
            _repo = repo;
        }

        public async Task<Jogo> GetByIdAsync(int id)
        {
            var jogo = await _repo.GetById(id);

            if (jogo is null)
                throw new Exception("Jogo não encontrado");

            return jogo;
        }

        public async Task<List<Jogo>> GetAllAsync()
        {
            var jogos = await _repo.GetAll();
            return jogos;
        }

        public async Task<bool> CreateAsync(Jogo jogoObj)
        {
            if (jogoObj.Titulo.IsNullOrEmpty())
                throw new Exception("Sem título");

            if (jogoObj.Preco <= 0)
                throw new Exception("Preço não informado");

            var ret = await _repo.Create(jogoObj);
            return ret;
        }

        public async Task<bool> UpdateAsync(Jogo jogoObj)
        {
            if (jogoObj.Id <= 0)
                throw new Exception("Jogo não encontrado");

            if (jogoObj.Titulo.IsNullOrEmpty())
                throw new Exception("Sem título");

            if (jogoObj.Preco <= 0)
                throw new Exception("Preço não informado");

            var existe = await _repo.GetById(jogoObj.Id);

            if (existe is null)
                throw new Exception("Jogo não encontrado");

            var ret = await _repo.Update(jogoObj);
            return ret;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            var ret = await _repo.DeleteById(id);
            return ret;
        }
    }
}

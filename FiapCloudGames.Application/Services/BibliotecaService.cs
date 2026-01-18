using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;

namespace FiapCloudGames.Application.Services
{
    public class BibliotecaService
    {
        public readonly IBibliotecaRepository _repo;
        public readonly IJogoRepository _repoGame;
        public readonly InfoToken _infoToken;

        public BibliotecaService(IBibliotecaRepository repo, IJogoRepository repoGame, InfoToken infoToken)
        {
            _repo = repo;
            _repoGame = repoGame;
            _infoToken = infoToken;
        }

        public async Task<List<JogoDto>> GetUserGamesByIdAsync(int id)
        {
            var games = await _repo.GetUserGamesById(id);
            return games;
        }

        public async Task<List<JogoDto>> GetMyGamesAsync()
        {
            var games = await _repo.GetMyGames();
            return games;
        }

        public async Task<bool> BuyGameAsync(int gameId)
        {
            var game = await _repoGame.GetById(gameId);
            var item = new ItemBiblioteca
            {
                JogoId = game.Id,
                UsuarioId = _infoToken.Id,
                PrecoPago = game.Preco,
                AdquiridoEm = DateTime.UtcNow
            };

            var result = await _repo.BuyGame(item);
            return result;
        }
    }
}

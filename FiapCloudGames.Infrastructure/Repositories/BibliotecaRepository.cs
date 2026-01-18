using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;
using FiapCloudGames.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class BibliotecaRepository : IBibliotecaRepository
    {
        public readonly FCGDbContext _db;
        public readonly InfoToken _infoToken;
        public BibliotecaRepository(FCGDbContext db, InfoToken infoToken)
        {
            _db = db;
            _infoToken = infoToken;
        }

        public async Task<List<JogoDto>> GetUserGamesById(int id)
        {
            var jogos = await (from userGame in _db.ItensBiblioteca
                               join games in _db.Jogos on userGame.JogoId equals games.Id
                               join users in _db.Usuarios on userGame.UsuarioId equals users.Id
                               select new JogoDto
                               {
                                   Id = games.Id,
                                   UsuarioId = users.Id,
                                   NomeComprador = users.Nome,
                                   Titulo = games.Titulo,
                                   Descricao = games.Descricao,
                                   PrecoPago = userGame.PrecoPago,
                                   AdquiridoEm = userGame.AdquiridoEm
                               }).ToListAsync();

            return jogos;
        }

        public async Task<List<JogoDto>> GetMyGames()
        {
            var meusJogos = await (from userGame in _db.ItensBiblioteca
                                   join games in _db.Jogos on userGame.JogoId equals games.Id
                                   select new JogoDto
                                   {
                                       Id = games.Id,
                                       UsuarioId = _infoToken.Id,
                                       NomeComprador = _infoToken.Nome,
                                       Titulo = games.Titulo,
                                       Descricao = games.Descricao,
                                       Preco = games.Preco,
                                       PrecoPago = userGame.PrecoPago,
                                       AdquiridoEm = userGame.AdquiridoEm
                                   }).ToListAsync();

            return meusJogos;
        }

        public async Task<bool> BuyGame(ItemBiblioteca item)
        {
            var existe = await _db.ItensBiblioteca.AnyAsync(x => x.UsuarioId == item.UsuarioId && x.JogoId == item.JogoId);
            
            if (existe)
                throw new Exception("Você já possui esse jogo em sua biblioteca");

            _db.ItensBiblioteca.Add(item);
            var changes = _db.SaveChanges();

            return changes > 0;
        }

    }
}

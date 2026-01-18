using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;
using FiapCloudGames.Domain.Interfaces.Services;
using FiapCloudGames.Domain.Utils;
using FiapCloudGames.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        public readonly FCGDbContext _db;
        public readonly InfoToken _infoToken;
        public JogoRepository(FCGDbContext db, InfoToken infoToken)
        {
            _db = db;
            _infoToken = infoToken;
        }

        public async Task<List<Jogo>> GetAll()
        {
            var listJogos = await _db.Jogos.ToListAsync();
            return listJogos;
        }

        public async Task<Jogo> GetById(int id)
        {
            var jogoObj = await _db.Jogos.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (jogoObj is null)
                throw new Exception("Jogo não encontrado");

            return jogoObj;
        }

        public async Task<bool> DeleteById(int id)
        {
            var changes = await _db.Jogos.Where(w => w.Id == id).ExecuteDeleteAsync();
            return changes > 0;
        }

        public async Task<bool> Create(Jogo jogoObj)
        {
            _db.Jogos.Add(jogoObj);
            var changes = _db.SaveChanges();

            return changes > 0;
        }

        public async Task<bool> Update(Jogo jogoObj)
        {
            _db.Jogos.Update(jogoObj);
            var changes = _db.SaveChanges();

            return changes > 0;
        }
    }
}

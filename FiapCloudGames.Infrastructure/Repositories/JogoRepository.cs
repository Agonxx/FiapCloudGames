using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;
using FiapCloudGames.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly FCGDbContext _db;

        public JogoRepository(FCGDbContext db)
        {
            _db = db;
        }

        public async Task<List<Jogo>> GetAll()
        {
            return await _db.Jogos.ToListAsync();
        }

        public async Task<Jogo> GetById(int id)
        {
            var jogo = await _db.Jogos.Where(x => x.Id == id).FirstOrDefaultAsync();
            return jogo;
        }

        public async Task<bool> DeleteById(int id)
        {
            var changes = await _db.Jogos.Where(w => w.Id == id).ExecuteDeleteAsync();
            return changes > 0;
        }

        public async Task<bool> Create(Jogo jogoObj)
        {
            _db.Jogos.Add(jogoObj);
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Update(Jogo jogoObj)
        {
            _db.Jogos.Update(jogoObj);
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }
    }
}

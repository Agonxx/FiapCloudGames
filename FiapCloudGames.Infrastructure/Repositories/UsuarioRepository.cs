using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;
using FiapCloudGames.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FCGDbContext _db;
        private readonly InfoToken _infoToken;

        public UsuarioRepository(FCGDbContext db, InfoToken infoToken)
        {
            _db = db;
            _infoToken = infoToken;
        }

        public async Task<Usuario> GetByEmailAndPassword(string email, string senha)
        {
            var usuarioObj = await _db.Usuarios.Where(w => EF.Functions.Like(w.Email, email) && w.SenhaHash == senha).FirstOrDefaultAsync();
            return usuarioObj;
        }

        public async Task<Usuario> GetMe()
        {
            var usuarioObj = await _db.Usuarios.Where(u => u.Id == _infoToken.Id).FirstOrDefaultAsync();
            return usuarioObj;
        }

        public async Task<Usuario> GetById(int id)
        {
            var usuarioObj = await _db.Usuarios.Where(u => u.Id == id).FirstOrDefaultAsync();
            return usuarioObj;
        }

        public async Task<bool> DeleteById(int id)
        {
            var changes = await _db.Usuarios.Where(w => w.Id == id).ExecuteDeleteAsync();
            return changes > 0;
        }

        public async Task<List<Usuario>> GetAll()
        {
            var listUsers = await (from users in _db.Usuarios
                                   select users).ToListAsync();

            return listUsers;
        }

        public async Task<bool> Create(Usuario usuarioObj)
        {
            _db.Usuarios.Add(usuarioObj);
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Update(Usuario usuarioObj)
        {
            _db.Usuarios.Update(usuarioObj);
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> EmailExists(string email, int excludeId = 0)
        {
            return await _db.Usuarios.AnyAsync(x => x.Email == email && x.Id != excludeId);
        }
    }
}

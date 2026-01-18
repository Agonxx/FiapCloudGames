using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;
using FiapCloudGames.Domain.Interfaces.Services;
using FiapCloudGames.Domain.Utils;
using FiapCloudGames.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly FCGDbContext _db;
        public readonly InfoToken _infoToken;
        public readonly ITokenService _token;
        public UsuarioRepository(FCGDbContext db, ITokenService tokenService, InfoToken infoToken)
        {
            _db = db;
            _token = tokenService;
            _infoToken = infoToken;
        }

        public async Task<string> Auth(string email, string senha)
        {
            var usuarioObj = await _db.Usuarios.Where(w => EF.Functions.Like(w.Email, email) && w.SenhaHash == senha).FirstOrDefaultAsync();

            if (usuarioObj is null)
                throw new Exception("Usuário não encontrado");

            var token = _token.GerarToken(usuarioObj.Id, usuarioObj.Nome, usuarioObj.Email, usuarioObj.Nivel, usuarioObj.CadastradoEm);
            return token;
        }

        public async Task<Usuario> GetMe()
        {
            var usuarioObj = await _db.Usuarios.Where(u => u.Id == _infoToken.Id).FirstOrDefaultAsync();

            if (usuarioObj is null)
                throw new Exception("Usuário não encontrado");

            return usuarioObj;
        }

        public async Task<Usuario> GetById(int id)
        {
            var usuarioObj = await _db.Usuarios.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (usuarioObj is null)
                throw new Exception("Usuário não encontrado");

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
            await ValidaEmailExiste(usuarioObj);

            _db.Usuarios.Add(usuarioObj);
            var changes = _db.SaveChanges();

            return changes > 0;
        }

        public async Task<bool> Update(Usuario usuarioObj)
        {
            await ValidaEmailExiste(usuarioObj);

            _db.Usuarios.Update(usuarioObj);
            var changes = _db.SaveChanges();

            return changes > 0;
        }

        private async Task ValidaEmailExiste(Usuario usuarioObj)
        {
            var existe = await _db.Usuarios.AnyAsync(x => x.Email == usuarioObj.Email && x.Id != usuarioObj.Id);

            if (existe)
                throw new Exception("Email já cadastrado no sistema");
        }
    }
}

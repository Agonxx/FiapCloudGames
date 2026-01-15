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
        public readonly ITokenService _token;
        public UsuarioRepository(FCGDbContext db, ITokenService tokenService)
        {
            _db = db;
            _token = tokenService;
        }

        public async Task<string> Autenticar(string email, string senha)
        {
            var usuarioObj = await _db.Usuarios.Where(w => EF.Functions.Like(w.Email, email) && w.Senha == senha).FirstOrDefaultAsync();

            if (usuarioObj is null)
                throw new Exception("Usuário não encontrado");

            var token = _token.GerarToken(usuarioObj.Id, usuarioObj.Nome, usuarioObj.Email, usuarioObj.Nivel, usuarioObj.CadastradoEm);
            return token;
        }

        public async Task<bool> Cadastrar(Usuario usuarioObj)
        {
            _db.Usuarios.Add(usuarioObj);
            _db.SaveChanges();

            return true;
        }
    }
}

using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;
using FiapCloudGames.Domain.Interfaces.Services;
using FiapCloudGames.Domain.Interfaces.Utils;

namespace FiapCloudGames.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly ICryptoUtils _cryptoUtils;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioRepository repo, ICryptoUtils cryptoUtils, ITokenService tokenService)
        {
            _repo = repo;
            _cryptoUtils = cryptoUtils;
            _tokenService = tokenService;
        }

        public async Task<string> AuthAsync(string email, string senha)
        {
            var senhaCrypto = _cryptoUtils.EncryptString(senha);
            var usuario = await _repo.GetByEmailAndPassword(email, senhaCrypto);

            if (usuario is null)
                throw new Exception("Usuário não encontrado");

            var token = _tokenService.GerarToken(usuario.Id, usuario.Nome, usuario.Email, usuario.Nivel, usuario.CadastradoEm);
            return token;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var user = await _repo.GetById(id);

            if (user is null)
                throw new Exception("Usuário não encontrado");

            return user;
        }

        public async Task<Usuario> GetMeAsync()
        {
            var user = await _repo.GetMe();

            if (user is null)
                throw new Exception("Usuário não encontrado");

            return user;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            var users = await _repo.GetAll();
            return users;
        }

        public async Task<bool> CreateAsync(Usuario usuarioObj)
        {
            var emailExiste = await _repo.EmailExists(usuarioObj.Email);

            if (emailExiste)
                throw new Exception("Email já cadastrado no sistema");

            usuarioObj.SenhaHash = _cryptoUtils.EncryptString(usuarioObj.SenhaHash);

            var ret = await _repo.Create(usuarioObj);
            return ret;
        }

        public async Task<bool> UpdateAsync(Usuario usuarioObj)
        {
            var existe = await _repo.GetById(usuarioObj.Id);

            if (existe is null)
                throw new Exception("Usuário não encontrado");

            var emailExiste = await _repo.EmailExists(usuarioObj.Email, usuarioObj.Id);

            if (emailExiste)
                throw new Exception("Email já cadastrado no sistema");

            usuarioObj.SenhaHash = _cryptoUtils.EncryptString(usuarioObj.SenhaHash);

            var ret = await _repo.Update(usuarioObj);
            return ret;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            if (id <= 0)
                return false;

            var ret = await _repo.DeleteById(id);
            return ret;
        }
    }
}

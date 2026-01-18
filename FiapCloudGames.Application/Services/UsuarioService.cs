using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;
using FiapCloudGames.Domain.Utils;

namespace FiapCloudGames.Application.Services
{
    public class UsuarioService
    {
        public readonly IUsuarioRepository _repo;
        public readonly CryptoUtils _cryptoUtils;
        public readonly InfoToken _infoToken;
        public UsuarioService(IUsuarioRepository repo, CryptoUtils cryptoUtils, InfoToken infoToken)
        {
            _repo = repo;
            _cryptoUtils = cryptoUtils;
            _infoToken = infoToken;
        }

        public async Task<string> AuthAsync(string email, string senha)
        {
            var senhaCrypto = _cryptoUtils.EncryptString(senha);
            var token = await _repo.Auth(email, senhaCrypto);

            return token;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var user = await _repo.GetById(id);
            return user;
        }

        public async Task<Usuario> GetMeAsync()
        {
            var user = await _repo.GetMe();
            return user;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            var users = await _repo.GetAll();
            return users;
        }

        public async Task<bool> CreateAsync(Usuario usuarioObj)
        {
            usuarioObj.SenhaHash = _cryptoUtils.EncryptString(usuarioObj.SenhaHash);

            var ret = await _repo.Create(usuarioObj);
            return ret;
        }

        public async Task<bool> UpdateAsync(Usuario usuarioObj)
        {
            usuarioObj.SenhaHash = _cryptoUtils.EncryptString(usuarioObj.SenhaHash);

            var ret = await _repo.Update(usuarioObj);
            return ret;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var ret = await _repo.DeleteById(id);
            return ret;
        }
    }
}

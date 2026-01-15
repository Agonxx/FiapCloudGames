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

        public async Task<string> Autenticar(string email, string senha)
        {
            var senhaCrypto = _cryptoUtils.EncryptString(senha);

            var token = await _repo.Autenticar(email, senhaCrypto);

            return token;
        }

        public async Task<bool> CadastrarAsync(Usuario usuarioObj)
        {
            usuarioObj.Senha = _cryptoUtils.EncryptString(usuarioObj.Senha);

            await _repo.Cadastrar(usuarioObj);
            return true;
        }
    }
}

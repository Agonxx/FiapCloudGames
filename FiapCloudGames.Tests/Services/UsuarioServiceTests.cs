using FiapCloudGames.Application.Services;
using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Enums;
using FiapCloudGames.Domain.Interfaces.Repositories;
using FiapCloudGames.Domain.Interfaces.Services;
using FiapCloudGames.Domain.Interfaces.Utils;
using Moq;

namespace FiapCloudGames.Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly Mock<ICryptoUtils> _cryptoMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _cryptoMock = new Mock<ICryptoUtils>();
            _tokenServiceMock = new Mock<ITokenService>();
            _service = new UsuarioService(_repoMock.Object, _cryptoMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task AuthAsync_DeveRetornarToken_QuandoCredenciaisValidas()
        {
            // Arrange
            var email = "admin@fcg.com";
            var senha = "A123";
            var senhaCrypto = "encrypted123";
            var tokenEsperado = "jwt-token-123";
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "Admin",
                Email = email,
                Nivel = ETipoUsuario.Administrador,
                CadastradoEm = DateTime.UtcNow
            };

            _cryptoMock.Setup(c => c.EncryptString(senha)).Returns(senhaCrypto);
            _repoMock.Setup(r => r.GetByEmailAndPassword(email, senhaCrypto)).ReturnsAsync(usuario);
            _tokenServiceMock.Setup(t => t.GerarToken(usuario.Id, usuario.Nome, usuario.Email, usuario.Nivel, usuario.CadastradoEm)).Returns(tokenEsperado);

            // Act
            var resultado = await _service.AuthAsync(email, senha);

            // Assert
            Assert.Equal(tokenEsperado, resultado);
            _cryptoMock.Verify(c => c.EncryptString(senha), Times.Once);
            _repoMock.Verify(r => r.GetByEmailAndPassword(email, senhaCrypto), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarUsuario_QuandoIdExiste()
        {
            // Arrange
            var usuarioEsperado = new Usuario
            {
                Id = 1,
                Nome = "Admin FCG",
                Email = "admin@fcg.com",
                Nivel = ETipoUsuario.Administrador,
                Ativo = true
            };

            _repoMock.Setup(r => r.GetById(1)).ReturnsAsync(usuarioEsperado);

            // Act
            var resultado = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuarioEsperado.Id, resultado.Id);
            Assert.Equal(usuarioEsperado.Nome, resultado.Nome);
            Assert.Equal(usuarioEsperado.Email, resultado.Email);
        }

        [Fact]
        public async Task GetMeAsync_DeveRetornarUsuarioLogado()
        {
            // Arrange
            var usuarioLogado = new Usuario
            {
                Id = 2,
                Nome = "User FCG",
                Email = "user@fcg.com",
                Nivel = ETipoUsuario.Usuario,
                Ativo = true
            };

            _repoMock.Setup(r => r.GetMe()).ReturnsAsync(usuarioLogado);

            // Act
            var resultado = await _service.GetMeAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuarioLogado.Id, resultado.Id);
            Assert.Equal(usuarioLogado.Email, resultado.Email);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarListaDeUsuarios()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "Admin", Email = "admin@fcg.com", Nivel = ETipoUsuario.Administrador },
                new Usuario { Id = 2, Nome = "User", Email = "user@fcg.com", Nivel = ETipoUsuario.Usuario }
            };

            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(usuarios);

            // Act
            var resultado = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
        }

        [Fact]
        public async Task CreateAsync_DeveCriptografarSenha_ECriarUsuario()
        {
            // Arrange
            var novoUsuario = new Usuario
            {
                Nome = "Novo Usuario",
                Email = "novo@fcg.com",
                SenhaHash = "senha123",
                Nivel = ETipoUsuario.Usuario
            };
            var senhaCrypto = "senhaCriptografada";

            _repoMock.Setup(r => r.EmailExists(novoUsuario.Email, 0)).ReturnsAsync(false);
            _cryptoMock.Setup(c => c.EncryptString("senha123")).Returns(senhaCrypto);
            _repoMock.Setup(r => r.Create(It.Is<Usuario>(u => u.SenhaHash == senhaCrypto))).ReturnsAsync(true);

            // Act
            var resultado = await _service.CreateAsync(novoUsuario);

            // Assert
            Assert.True(resultado);
            Assert.Equal(senhaCrypto, novoUsuario.SenhaHash);
            _cryptoMock.Verify(c => c.EncryptString("senha123"), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveCriptografarSenha_EAtualizarUsuario()
        {
            // Arrange
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "Usuario Atualizado",
                Email = "atualizado@fcg.com",
                SenhaHash = "novaSenha",
                Nivel = ETipoUsuario.Usuario
            };
            var senhaCrypto = "novaSenhaCrypto";

            _repoMock.Setup(r => r.GetById(usuario.Id)).ReturnsAsync(usuario);
            _repoMock.Setup(r => r.EmailExists(usuario.Email, usuario.Id)).ReturnsAsync(false);
            _cryptoMock.Setup(c => c.EncryptString("novaSenha")).Returns(senhaCrypto);
            _repoMock.Setup(r => r.Update(It.Is<Usuario>(u => u.SenhaHash == senhaCrypto))).ReturnsAsync(true);

            // Act
            var resultado = await _service.UpdateAsync(usuario);

            // Assert
            Assert.True(resultado);
            Assert.Equal(senhaCrypto, usuario.SenhaHash);
        }

        [Fact]
        public async Task DeleteByIdAsync_DeveRetornarTrue_QuandoUsuarioExcluido()
        {
            // Arrange
            _repoMock.Setup(r => r.DeleteById(1)).ReturnsAsync(true);

            // Act
            var resultado = await _service.DeleteByIdAsync(1);

            // Assert
            Assert.True(resultado);
            _repoMock.Verify(r => r.DeleteById(1), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_DeveRetornarFalse_QuandoUsuarioNaoExiste()
        {
            // Arrange
            _repoMock.Setup(r => r.DeleteById(999)).ReturnsAsync(false);

            // Act
            var resultado = await _service.DeleteByIdAsync(999);

            // Assert
            Assert.False(resultado);
        }
    }
}

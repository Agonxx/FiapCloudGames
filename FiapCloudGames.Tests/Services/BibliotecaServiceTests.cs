using FiapCloudGames.Application.Services;
using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Repositories;
using Moq;

namespace FiapCloudGames.Tests.Services
{
    public class BibliotecaServiceTests
    {
        private readonly Mock<IBibliotecaRepository> _repoMock;
        private readonly Mock<IJogoRepository> _repoGameMock;
        private readonly InfoToken _infoToken;
        private readonly BibliotecaService _service;

        public BibliotecaServiceTests()
        {
            _repoMock = new Mock<IBibliotecaRepository>();
            _repoGameMock = new Mock<IJogoRepository>();
            _infoToken = new InfoToken { Id = 1, Nome = "Usuario Teste", Email = "teste@fcg.com" };
            _service = new BibliotecaService(_repoMock.Object, _repoGameMock.Object, _infoToken);
        }

        [Fact]
        public async Task GetUserGamesByIdAsync_DeveRetornarJogosDoUsuario()
        {
            // Arrange
            var userId = 1;
            var jogosEsperados = new List<JogoDto>
            {
                new JogoDto
                {
                    Id = 1,
                    UsuarioId = userId,
                    NomeComprador = "Usuario",
                    Titulo = "Elden Ring",
                    Descricao = "RPG",
                    Preco = 249.90m,
                    PrecoPago = 249.90m,
                    AdquiridoEm = DateTime.UtcNow
                }
            };

            _repoMock.Setup(r => r.GetUserGamesById(userId)).ReturnsAsync(jogosEsperados);

            // Act
            var resultado = await _service.GetUserGamesByIdAsync(userId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal("Elden Ring", resultado[0].Titulo);
        }

        [Fact]
        public async Task GetUserGamesByIdAsync_DeveRetornarListaVazia_QuandoUsuarioNaoTemJogos()
        {
            // Arrange
            var userId = 999;
            _repoMock.Setup(r => r.GetUserGamesById(userId)).ReturnsAsync(new List<JogoDto>());

            // Act
            var resultado = await _service.GetUserGamesByIdAsync(userId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }

        [Fact]
        public async Task GetMyGamesAsync_DeveRetornarJogosDoUsuarioLogado()
        {
            // Arrange
            var meusJogos = new List<JogoDto>
            {
                new JogoDto
                {
                    Id = 1,
                    UsuarioId = _infoToken.Id,
                    NomeComprador = _infoToken.Nome,
                    Titulo = "EA FC 26",
                    PrecoPago = 249.90m
                },
                new JogoDto
                {
                    Id = 2,
                    UsuarioId = _infoToken.Id,
                    NomeComprador = _infoToken.Nome,
                    Titulo = "Elden Ring",
                    PrecoPago = 199.90m
                }
            };

            _repoMock.Setup(r => r.GetMyGames()).ReturnsAsync(meusJogos);

            // Act
            var resultado = await _service.GetMyGamesAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
        }

        [Fact]
        public async Task BuyGameAsync_DeveComprarJogo_QuandoJogoExiste()
        {
            // Arrange
            var gameId = 1;
            var jogo = new Jogo
            {
                Id = gameId,
                Titulo = "Elden Ring",
                Preco = 249.90m
            };

            _repoGameMock.Setup(r => r.GetById(gameId)).ReturnsAsync(jogo);
            _repoMock.Setup(r => r.ExistsInLibrary(_infoToken.Id, gameId)).ReturnsAsync(false);
            _repoMock.Setup(r => r.BuyGame(It.Is<ItemBiblioteca>(i =>
                i.JogoId == gameId &&
                i.UsuarioId == _infoToken.Id &&
                i.PrecoPago == jogo.Preco
            ))).ReturnsAsync(true);

            // Act
            var resultado = await _service.BuyGameAsync(gameId);

            // Assert
            Assert.True(resultado);
            _repoGameMock.Verify(r => r.GetById(gameId), Times.Once);
            _repoMock.Verify(r => r.ExistsInLibrary(_infoToken.Id, gameId), Times.Once);
            _repoMock.Verify(r => r.BuyGame(It.IsAny<ItemBiblioteca>()), Times.Once);
        }

        [Fact]
        public async Task BuyGameAsync_DeveCriarItemBiblioteca_ComDadosCorretos()
        {
            // Arrange
            var gameId = 2;
            var jogo = new Jogo
            {
                Id = gameId,
                Titulo = "EA FC 26",
                Preco = 299.90m
            };

            ItemBiblioteca? itemCapturado = null;

            _repoGameMock.Setup(r => r.GetById(gameId)).ReturnsAsync(jogo);
            _repoMock.Setup(r => r.ExistsInLibrary(_infoToken.Id, gameId)).ReturnsAsync(false);
            _repoMock.Setup(r => r.BuyGame(It.IsAny<ItemBiblioteca>()))
                .Callback<ItemBiblioteca>(item => itemCapturado = item)
                .ReturnsAsync(true);

            // Act
            await _service.BuyGameAsync(gameId);

            // Assert
            Assert.NotNull(itemCapturado);
            Assert.Equal(gameId, itemCapturado.JogoId);
            Assert.Equal(_infoToken.Id, itemCapturado.UsuarioId);
            Assert.Equal(jogo.Preco, itemCapturado.PrecoPago);
        }

        [Fact]
        public async Task BuyGameAsync_DeveRetornarFalse_QuandoCompraNaoRealizada()
        {
            // Arrange
            var gameId = 1;
            var jogo = new Jogo { Id = gameId, Titulo = "Jogo", Preco = 100m };

            _repoGameMock.Setup(r => r.GetById(gameId)).ReturnsAsync(jogo);
            _repoMock.Setup(r => r.ExistsInLibrary(_infoToken.Id, gameId)).ReturnsAsync(false);
            _repoMock.Setup(r => r.BuyGame(It.IsAny<ItemBiblioteca>())).ReturnsAsync(false);

            // Act
            var resultado = await _service.BuyGameAsync(gameId);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task BuyGameAsync_DeveLancarExcecao_QuandoJogoNaoExiste()
        {
            // Arrange
            var gameId = 999;
            _repoGameMock.Setup(r => r.GetById(gameId)).ReturnsAsync((Jogo?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.BuyGameAsync(gameId));
            Assert.Equal("Jogo não encontrado", ex.Message);
        }

        [Fact]
        public async Task BuyGameAsync_DeveLancarExcecao_QuandoJogoJaExisteNaBiblioteca()
        {
            // Arrange
            var gameId = 1;
            var jogo = new Jogo { Id = gameId, Titulo = "Elden Ring", Preco = 249.90m };

            _repoGameMock.Setup(r => r.GetById(gameId)).ReturnsAsync(jogo);
            _repoMock.Setup(r => r.ExistsInLibrary(_infoToken.Id, gameId)).ReturnsAsync(true);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.BuyGameAsync(gameId));
            Assert.Equal("Você já possui esse jogo em sua biblioteca", ex.Message);
        }
    }
}

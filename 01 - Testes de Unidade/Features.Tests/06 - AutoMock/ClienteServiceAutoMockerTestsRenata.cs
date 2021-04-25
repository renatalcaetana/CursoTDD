using System.Linq;
using System.Threading;
using Features.Clientes;
using MediatR;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollectionRenata))]
    public class ClienteServiceAutoMockerTestsRenata
    {
        private readonly ClienteTestsBogusFixtureRenata _clienteTestsBogus;

        //  readonly ClienteTestsBogusFixture _clienteTestsBogus;


        private readonly ClienteService clienteService;
        public ClienteServiceAutoMockerTestsRenata(ClienteTestsBogusFixtureRenata clienteTestsBogus)
        {
            _clienteTestsBogus = clienteTestsBogus;
            clienteService = _clienteTestsBogus.ObterService();
        }


        [Fact(DisplayName = "Adicionar Cliente com Sucesso1")]
        [Trait("Renata Categoria", "Cliente Service AutoMock Tests1")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso1()
        {
            //arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
          
            //act
            clienteService.Adicionar(cliente);

            //assert
            Assert.True(cliente.EhValido());

            _clienteTestsBogus.mocker.GetMock<IClienteRepository>().Verify(l => l.Adicionar(cliente), Times.Once);
            _clienteTestsBogus.mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Renata Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            _clienteTestsBogus.mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTestsBogus.mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Renata Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteInvalido();

            var clienteService = _clienteTestsBogus.ObterService();
            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            _clienteTestsBogus.mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTestsBogus.mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Renata Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
       
            _clienteTestsBogus.mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clienteTestsBogus.ObterClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert 
            _clienteTestsBogus.mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }
    }
}
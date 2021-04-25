using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteCollection))]
    public class ClientesTesteValidoRenata
    {
        private readonly ClienteFixtureTests _clienteFixtureTests;

        public ClientesTesteValidoRenata(ClienteFixtureTests clienteFixtureTest)
        {
            _clienteFixtureTests = clienteFixtureTest;
        }
        [Fact(DisplayName = "Novo Cliente Válido renata")]
        [Trait("Categoria", "Cliente trait testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            //Arrange
            var cliente = _clienteFixtureTests.ClienteValido();
            //Act
            var result = cliente.EhValido();

            //Assert
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);

        }

    }
}

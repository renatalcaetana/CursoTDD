using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteCollection))]
    public class ClientesTesteInvalidoRenata
    {
        private readonly ClienteFixtureTests _clienteFixtureTests;

        public ClientesTesteInvalidoRenata(ClienteFixtureTests clienteFixtureTests)
        {
            _clienteFixtureTests = clienteFixtureTests;
        }
        [Fact(DisplayName = "Novo Cliente Inválido Renata")]
        [Trait("Categoria", "Cliente trait testes")]
        public void Cliente_Novo_DeveEstarInvalido()
        {

            //Arrange
            var cliente = _clienteFixtureTests.ClienteInValido();
            //Act
            var result = cliente.EhValido();

            //Assert
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);

        }
    }
}

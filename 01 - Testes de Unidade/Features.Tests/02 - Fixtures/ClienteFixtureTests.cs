using Features.Clientes;
using System;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteCollection))]

    public class ClienteCollection : ICollectionFixture<ClienteFixtureTests>
    { }
    public class ClienteFixtureTests : IDisposable
    {

        public Cliente ClienteValido()
        {
            var cliente = new Cliente(
                          Guid.NewGuid(),
                "Eduardo Pires"
                , "Pires"
                , DateTime.Now.AddYears(-30), "Edu@edu.com.br"
                , true
                , DateTime.Now);
            return cliente;
        }
        public Cliente ClienteInValido()
        {
            var cliente = new Cliente(
                          Guid.NewGuid(),
                ""
                , "Pires"
                , DateTime.Now.AddYears(-5), "Edu@edu.com.br"
                , true
                , DateTime.Now);
            return cliente;
        }
        void IDisposable.Dispose()
        {
        }
    }
}

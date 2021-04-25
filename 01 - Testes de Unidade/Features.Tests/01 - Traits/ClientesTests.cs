using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests
{
    public class Clientes
    {
        [Fact(DisplayName = "Novo Cliente Válido111")]
        [Trait("Categoria", "Cliente trait testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            //Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Eduardo Pires"
                , "Pires"
                , DateTime.Now.AddYears(-30), "Edu@edu.com.br"
                , true
                , DateTime.Now);
            //Act
            var result = cliente.EhValido();

            //Assert
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);

        }

        [Fact(DisplayName = "Novo Cliente Inválido111")]
        [Trait("Categoria", "Cliente trait testes")]
        public void Cliente_Novo_DeveEstarInvalido()
        {

            //Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                ""
                , ""
                , DateTime.Now.AddYears(-5), "Edu@edu.com.br"
                , true
                , DateTime.Now);
            //Act
            var result = cliente.EhValido();

            //Assert
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);

        }
    }
}

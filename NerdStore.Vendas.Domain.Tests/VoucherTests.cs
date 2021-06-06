using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
   public class VoucherTests
    {

        [Fact(DisplayName = "Validar voucher tipo valor valido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher
            {
                Codigo = "Promocão",
                ValorDesconto = 15,
                PercentualDesconto = null,
                Quantidade = 1,
                DataValidade = null,
                Ativo = true,
                Utilizado = false
            };

            // Act
            var resultado = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(resultado.IsValid);

        }
    }
}

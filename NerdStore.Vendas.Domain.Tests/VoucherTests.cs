using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {

        [Fact(DisplayName = "Validar voucher tipo valor valido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("Promocão",15, TipoDescontoVoucher.Porcentagem, 1, 1, DateTime.Now.AddDays(-1), true, false);

            // Act
            var resultado = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(resultado.IsValid);

        }
    }
}

using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoItemTests
    {
        [Fact(DisplayName = "Novo item pedido com unidades abaixo do permitido")]
        [Trait("Categoria", "Novo Item Pedido Tests")]
        public void AdicionaiItemPedido_ItemAbaixodoPermitido_DeveRetornarException()
        {
           
            // Act & Assert
            Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(), "Produto Teste",Pedido.Min_UNIDADES_ITEM -1, 100 ));

        }
    }
}

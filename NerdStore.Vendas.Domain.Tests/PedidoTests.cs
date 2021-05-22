using NerdStore.Core.DomainObjects;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar item novo pedido")]
        [Trait("Categoria", "Pedido Testes")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(),"Produto Teste",2,100);

            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);

                
        }

        [Fact(DisplayName = "Adicionar item pedido existente")]
        [Trait("Categoria", "Pedido Testes")]
        public void Trocar_Nome_Metodo()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var itemPedido = new PedidoItem(produtoId, "Chocolate", 1, 100);
            var itemPedido2 = new PedidoItem(produtoId, "Chocolate", 2, 100);

            // Act
            pedido.AdicionarItem(itemPedido2);
            pedido.AdicionarItem(itemPedido);

            // Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(x=>x.ProdutoId == produtoId).Quantidade);

        }
        [Fact(DisplayName = "Adicionar item pedido acima do permitido")]
        [Trait("Categoria", "Pedido Tests")]
        public void AdicionaiItemPedido_ItemAcimadoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Chocolate", Pedido.Max_UNIDADES_ITEM + 1, 100);
            
            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));
       
        }
       
    }
}

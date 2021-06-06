using NerdStore.Core.DomainObjects;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar item novo pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
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
        [Trait("Categoria", "Vendas - Pedido")]
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
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionaiItemPedido_ItemAcimadoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Chocolate", Pedido.Max_UNIDADES_ITEM + 1, 100);
            
            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));
       
        }
        [Fact(DisplayName = "Adicionar item fora da quantidade permitida")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ForadaQuantidadePermitida_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Chocolote", 1, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Chocolote", Pedido.Max_UNIDADES_ITEM + 1, 100);


            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem2));

            // Assert

        }
        [Fact(DisplayName = "Remover item inexistente na lista")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemInexistentenaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Chocolote", 1, 100);
          
            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItem));

            // Assert

        }
        [Fact(DisplayName = "Remover item pedido existente na lista")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemExistentenaLista_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Chocolote", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Leite", 3, 15);

            pedido.AdicionarItem(pedidoItem);
            pedido.AdicionarItem(pedidoItem2);


            var valorTotal = pedidoItem2.Quantidade * pedidoItem2.ValorUnitario;

            pedido.RemoverItem(pedidoItem);
            // Act & Assert
            Assert.Equal(valorTotal, pedido.ValorTotal);

            // Assert

        }
    }
}

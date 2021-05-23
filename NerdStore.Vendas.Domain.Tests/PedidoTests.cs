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
        [Fact(DisplayName = "Atualizar pedido inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_Inexistente_DeveRetonarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Leite", 1, 100);
            // Act


            // Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItemPedido(pedidoItem));            
        }
        [Fact(DisplayName = "Item valido deve atualizar a quantidade")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Leite", 1, 5);
            pedido.AdicionarItem(pedidoItem);
            var novoPedido = new PedidoItem(produtoId, "Leite", 1, 4);
            var quantidadeExpected = novoPedido.Quantidade;

            // Act
            pedido.AtualizarItemPedido(novoPedido);

            // Assert
            Assert.Equal(quantidadeExpected, pedido.PedidoItems.FirstOrDefault(x=> x.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = "Para produtos diferentes deve atualizar valor total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_PedidoComProdutosDiferentes_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();

            var pedidoItem1 = new PedidoItem(produtoId, "Leite", 1, 10);
            var pedidoItemAtualizar = new PedidoItem(produtoId, "Leite", 2, 10);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Chocolate", 2, 10);
        

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);
            // Act
            var valorTotalExpertect = (pedidoItemAtualizar.Quantidade * pedidoItemAtualizar.ValorUnitario) +
                                      (pedidoItem2.Quantidade * pedidoItem2.ValorUnitario);

            pedido.AtualizarItemPedido(pedidoItemAtualizar);
            // Assert
            Assert.Equal(valorTotalExpertect, pedido.ValorTotal);

        }
        [Fact(DisplayName = "Para produtos diferentes deve atualizar valor total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemUnidadeAcimaPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();

            var pedidoItem1 = new PedidoItem(produtoId, "Leite", 1, 10);
            var pedidoItemAtualizar = new PedidoItem(produtoId, "Leite", Pedido.Max_UNIDADES_ITEM, 10);
          
            pedido.AdicionarItem(pedidoItem1);
            
            // Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItemPedido(pedidoItemAtualizar));

        }
    }
}

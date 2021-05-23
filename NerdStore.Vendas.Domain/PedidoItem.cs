using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace NerdStore.Vendas.Domain
{
    public class PedidoItem
    {
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, int valorUnitario)
        {

            if (quantidade < Pedido.Min_UNIDADES_ITEM) throw new DomainException($"Mínimo de {Pedido.Min_UNIDADES_ITEM} unidades por produto");
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;

        }
        internal void AdicionarQuantidade(int quantidade)
        {
            Quantidade += quantidade;
        }
        public decimal CalcularValor() => (Quantidade * ValorUnitario);

    }

}

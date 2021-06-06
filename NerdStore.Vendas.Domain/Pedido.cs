using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain
{
    public class Pedido
    {
        public static int Max_UNIDADES_ITEM => 15;
        public static int Min_UNIDADES_ITEM => 1;

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }
        public decimal ValorTotal { get; private set; }

        public Guid ClienteId { get; private set; }

        public PedidoStatus PedidoStatusi { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;

        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        private void CalcularValor()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());

        }
        private void ValidarQuantidadePedidoPermitida(PedidoItem pedidoItem)
        {
            var quantidadePedido = pedidoItem.Quantidade;

            if (ExisteItemPedido(pedidoItem))
            {
                var pedidoExistente = _pedidoItems.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId);

                quantidadePedido += pedidoExistente.Quantidade;
                               
            }
            if (quantidadePedido > Max_UNIDADES_ITEM) throw new DomainException($"Máximo de {Max_UNIDADES_ITEM} unidades por produto");

        }

        private bool ExisteItemPedido(PedidoItem pedidoItem) => _pedidoItems.Any(x => x.ProdutoId == pedidoItem.ProdutoId);
     
        public void AdicionarItem(PedidoItem pedidoItem)
        {
            ValidarQuantidadePedidoPermitida(pedidoItem);

            if (ExisteItemPedido(pedidoItem))
            {
                            
                var itemExistente = _pedidoItems.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId);

                itemExistente.AdicionarQuantidade(pedidoItem.Quantidade);

                pedidoItem = itemExistente;
                _pedidoItems.Remove(itemExistente);
            }

            _pedidoItems.Add(pedidoItem);
            CalcularValor();

        }
        private void ValidarItemPedidoInexistente(PedidoItem pedidoItem)
        {
            if (!ExisteItemPedido(pedidoItem))
            {
               throw new DomainException("Pedido inexistente!");
            }
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            ValidarItemPedidoInexistente(pedidoItem);

            _pedidoItems.Remove(pedidoItem);

            CalcularValor();
        }

        public void TornarRascunho()
        {
            PedidoStatusi = PedidoStatus.Rascunho;
        }


        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId
                };
                pedido.TornarRascunho();
                return pedido;
            }


        }

    }

    public enum PedidoStatus
    {
        Rascunho = 0,
        Iniciado = 1,
        Pago = 4,
        Entregue = 5,
        Cancelado = 6
    }
}

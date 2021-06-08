﻿using System;
using FluentValidation;
using FluentValidation.Results;

namespace NerdStore.Vendas.Domain
{
    public class Voucher
    {

        public string Codigo { get; private set; }
        public decimal ValorDesconto { get; private set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
        public decimal? PercentualDesconto { get; private set; }
        public int Quantidade { get; private set; }
        public DateTime? DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }

        public ValidationResult ValidarSeAplicavel()
        {
            return new VoucherAplicavelValidation().Validate(this);
        }

        public Voucher(string codigo, decimal valorDesconto, TipoDescontoVoucher tipoDescontoVoucher, decimal porcentalDesconto, int quantidade, DateTime dataValidade, bool ativo, bool utilizado)
        {
            Codigo = codigo;
            ValorDesconto = valorDesconto;
            TipoDescontoVoucher = tipoDescontoVoucher;
            PercentualDesconto = porcentalDesconto;
            Quantidade = quantidade;
            DataValidade = dataValidade;
            Ativo = ativo;
            Utilizado = utilizado;

        }
    }
    public class VoucherAplicavelValidation : AbstractValidator<Voucher>
    {
        public static string CodigoErroMsg => "Voucher sem codigo valido";

        public static string DetalheValidade => "Voucher está expirado";

        public static string AtivoErroMsg => "Esse voucher não é mais válido";

        public static string UtilizadoErroMsg => "Este voucher já foi utlizado";

        public static string QuantidadeErroMg => "Este voucher nao está mais disponível";

        public static string ValorDescontoErroMsg => "O valor de desconto precisa ser superior a 0";

        public static string PercentualDescontoErroMsg => "O valor da porcentagme de desconto precisa ser superior a X";

        public VoucherAplicavelValidation()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty()
                .WithMessage(CodigoErroMsg);

            RuleFor(c => c.DataValidade)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage(DetalheValidade);

            RuleFor(c => c.Ativo)
                .Equal(true)
                .WithMessage(AtivoErroMsg);

            RuleFor(c=>c.Utilizado)
                .Equal(false)
                .WithMessage(UtilizadoErroMsg);

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(QuantidadeErroMg);

            When(f => f.TipoDescontoVoucher == TipoDescontoVoucher.Valor,  () =>
              {
                  RuleFor(f => f.ValorDesconto)
                  .NotNull()
                  .WithMessage(ValorDescontoErroMsg)
                  .GreaterThan(1)
                  .WithMessage(ValorDescontoErroMsg);
              });

            When(f => f.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem, () =>
            {
                RuleFor(f => f.ValorDesconto)
                .NotNull()
                .WithMessage(PercentualDescontoErroMsg)
                .GreaterThan(0)
                .WithMessage(PercentualDescontoErroMsg);
            });


        }

        protected static bool DataVencimentoSuperiorAtual(DateTime? dataValidade)
        {
            return dataValidade >= DateTime.Now;
        }
    }
    public enum TipoDescontoVoucher
    {
        Porcentagem = 0,
        Valor = 1


    }
}

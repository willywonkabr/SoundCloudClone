using SoundCloudClone.Domain.Conta.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.Domain.Transacao
{
    public class Cartao
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; }
        public decimal Limite { get; set; }
        public String Numero { get; set; }
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();
        private const int TEMPO_INTERVALO_TRANSACAO = -2;
        private const int TRANSACAO_REPETIDA = 1;
        public void CriarTransacao(string vendedor, decimal valor, string descricao)
        {
            CartaoException validationErrors = new CartaoException();

            IsCartaoAtivo(validationErrors);

            Transacao transacao = new Transacao();
            transacao.Vendedor = vendedor;
            transacao.Valor = valor;
            transacao.Descricao = descricao;
            transacao.Data = DateTime.Now;

            VerificarLimiteDisponivel(transacao, validationErrors);

            ValidarTransacao(transacao, validationErrors);

            validationErrors.ValidateAndThrow();

            transacao.Id = Guid.NewGuid();

            Limite = Limite - transacao.Valor;

            Transacoes.Add(transacao);
        }

        private void IsCartaoAtivo(CartaoException validationErrors)
        {
            if (Ativo == false)
            {
                validationErrors.AddError(new Core.Exceptions.BusinessValidation()
                {
                    ErrorDescription = "O cartão não se encontra ativo.",
                    ErrorName = nameof(Cartao)
                });
            }
        }

        private void VerificarLimiteDisponivel(Transacao transacao, CartaoException validationErrors)
        {
            if (transacao.Valor > Limite)
            {
                validationErrors.AddError(new Core.Exceptions.BusinessValidation()
                {
                    ErrorDescription = "O cartão está sem limite para realizar esta transação",
                    ErrorName = nameof(Cartao)
                });
            }
        }

        private void ValidarTransacao(Transacao transacao, CartaoException validationErrors)
        {
            var ultimasTransacao = Transacoes.Where(transacao => transacao.Data >= DateTime.Now.AddMinutes(TEMPO_INTERVALO_TRANSACAO));

            if (ultimasTransacao?.Count() >= 3)
            {
                validationErrors.AddError(new Core.Exceptions.BusinessValidation()
                {
                    ErrorDescription = "O cartão foi utilizado muitas vezes durante um curto período de tempo",
                    ErrorName = nameof(Cartao)
                });
            }

            var transacaoRepetida = ultimasTransacao.Where(transacao => transacao.Vendedor.ToUpper() == transacao.Vendedor.ToUpper() && transacao.Valor == transacao.Valor).Count() == TRANSACAO_REPETIDA;

            if (transacaoRepetida)
            {
                validationErrors.AddError(new Core.Exceptions.BusinessValidation()
                {
                    ErrorDescription = "Transação duplicada, mesmo cartão e mesmo vendedor",
                    ErrorName = nameof(Cartao)
                });
            }
        }
    }
}

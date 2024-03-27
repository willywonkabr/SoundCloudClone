using SoundCloudClone.Domain.Conta.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.Domain.Conta
{
    public class Cartao
    {
        private Guid Id { get; set; }
        private Boolean Ativo { get; set; }
        private Decimal Limite { get; set; }
        private List<Transacao> Transacoes { get; set; } = new List<Transacao>();
        private const int TEMPO_INTERVALO_TRANSACAO = -2;
        private const int TRANSACAO_REPETIDA = 1;
        public void CriarTransacao(String vendedor, decimal valor, string descricao)
        {
            CartaoException validationErrors = new CartaoException();

            this.IsCartaoAtivo(validationErrors);

            Transacao transacao = new Transacao();
            transacao.Vendedor = vendedor;
            transacao.Valor = valor;
            transacao.Descricao = descricao;
            transacao.Data = DateTime.Now;

            this.VerificarLimiteDisponivel(transacao, validationErrors);

            this.ValidarTransacao(transacao, validationErrors);

            validationErrors.ValidateAndThrow();

            transacao.Id = Guid.NewGuid();

            this.Limite = this.Limite - transacao.Valor;

            this.Transacoes.Add(transacao);
        }

        private void IsCartaoAtivo(CartaoException validationErrors)
        {
            if (this.Ativo == false)
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
            if (transacao.Valor > this.Limite)
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
            var ultimasTransacao = this.Transacoes.Where(transacao => transacao.Data >= DateTime.Now.AddMinutes(TEMPO_INTERVALO_TRANSACAO));

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

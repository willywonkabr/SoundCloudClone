using SoundCloudClone.Domain.Conta;
using SoundCloudClone.Domain.Streaming;
using SoundCloudClone.Domain.Transacao;
using SoundCloudClone.Repository.Conta;
using SoundCloudClone.Repository.Streaming;
using SoundCloudClone.Repository.Transacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.Application.Conta
{
    public class UsuarioService
    {
        private UsuarioRepository usuarioRepository;
        private PlanoRepository planoRepository;
        private BandaRepository bandaRepository;

        public UsuarioService(UsuarioRepository usuarioRepository, PlanoRepository planoRepository, BandaRepository bandaRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.planoRepository = planoRepository;
            this.bandaRepository = bandaRepository;
        }

        public Usuario CriarConta(String nome, Guid planoId, Cartao cartao)
        {
            Plano plano = this.planoRepository.ObterPlanoById(planoId);

            if (plano == null)
                throw new Exception("Plano não encontrado.");

            Usuario usuario = new Usuario();
            usuario.Criar(nome, plano, cartao);

            this.usuarioRepository.Save(usuario);

            var notificacao = new Notificacao();
            notificacao.IdUsuario = usuario.Id;
            notificacao.Message = $"Nós do SoundCloud te desejamos boas-vinda. Debitamos o valor de R${plano.Valor.ToString("N2")} no seu cartão";

            //AzureServiceBusService notificationService = new AzureServiceBusService();
            //notificationService.SendMessage(notificacao).Wait();

            return usuario;
        }

        public Usuario ObterUsuario(Guid id)
        {
            var usuario = this.usuarioRepository.ObterUsuario(id);
            return usuario;
        }

        public void FavoritarMusica(Guid id, Guid idMusica)
        {
            var usuario = this.usuarioRepository.ObterUsuario(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            var musica = VerificarMusica(idMusica);

            usuario.DesfavoritarMusica(musica);

            this.usuarioRepository.Update(usuario);
        }

        public void DesfavoritarMusica(Guid id, Guid idMusica)
        {
            var usuario = this.usuarioRepository.ObterUsuario(id);

            if (usuario == null) throw new Exception("Usuário não encontrado");

            var musica = VerificarMusica(idMusica);

            usuario.DesfavoritarMusica(musica);

            this.usuarioRepository.Update(usuario);
        }

        private Musica VerificarMusica(Guid idMusica)
        {
            var musica = this.bandaRepository.ObterMusica(idMusica);

            if (musica == null)
                throw new Exception("Música a ser favoritada não encontrada.");

            return musica;
        }
    }
}

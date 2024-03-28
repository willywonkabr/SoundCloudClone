using SoundCloudClone.Domain.Streaming;
using SoundCloudClone.Domain.Transacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.Domain.Conta
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }
        public List<Cartao> Cartoes { get; set; } = new List<Cartao>(); 
        public List<Playlist> Playlists { get; set; } = new List<Playlist>(); 
        public List<Assinatura> Assinaturas { get; set;} = new List<Assinatura>();
    
        public void Criar(String nome, Plano plano, Cartao cartao)
        {
            this.Nome = nome;

            this.AssinarPlano(plano, cartao);

            this.AdicionarCartao(cartao);

            this.CriarPlaylist();
        }

        private void AssinarPlano(Plano plano, Cartao cartao)
        {
            cartao.CriarTransacao(plano.Nome, plano.Valor, plano.Descricao);

            if (this.Assinaturas.Count > 0 && this.Assinaturas.Any(plano => plano.Ativo))
            {
                var planoAtivo = this.Assinaturas.FirstOrDefault(plano => plano.Ativo);
                planoAtivo.Ativo = false;
            }
        }

        private void AdicionarCartao(Cartao cartao)
        {
            this.Cartoes.Add(cartao);
        }

        public void CriarPlaylist(string nome = "Favoritas", bool publica = false)
        {
            this.Playlists.Add(new Playlist()
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Publica = publica,
                Usuario = this,
            });
        }

        public void FavoritarMusica(Musica musica, string playlistNome = "Favoritas")
        {
            var playlist = this.Playlists.FirstOrDefault(playlist => playlist.Nome == playlistNome);

            if (playlist == null)
            {
                throw new Exception("Playlist não encontrada");
            }

            playlist.Musicas.Add(musica);
        }

        public void DesfavoritarMusica(Musica musica, string playlistNome = "Favoritas")
        {
            var playlist = this.Playlists.FirstOrDefault(playlist => playlist.Nome == playlistNome);

            if (playlist == null)
                throw new Exception("Playlist não encontrada");

            var musicaFavorita = playlist.Musicas.FirstOrDefault(musica => musica.Id == musica.Id);

            if (musicaFavorita == null)
                throw new Exception("Playlist não encontrada");

            playlist.Musicas.Remove(musica);
        }
    }
}

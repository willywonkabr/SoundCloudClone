using SoundCloudClone.Domain.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.Domain.Conta
{
    public class Playlist
    {
        private Guid Id { get; set; }
        private string Nome { get; set; }
        private bool Publica { get; set; }
        private Usuario Usuario { get; set; }
        private List<Musica> Musicas { get; set; }
        public Playlist()
        {
            Musicas = new List<Musica>();
        }
    }
}

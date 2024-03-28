﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.API.Response
{
    public class UsuarioResponse
    {
        public Guid Id { get ; set; }
        public string Nome { get; set; }
        public Guid PlnaoId { get; set; }

        public List<PlaylistResponse> Playlists { get; set; } = new List<PlaylistResponse>();
    }

    public class PlaylistResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<MusicaResponse> Musica { get; set; } = new List<MusicaResponse>();   
    }

    public class MusicaResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Decimal Duracao { get; set; }
    }
}

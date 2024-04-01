using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundCloudClone.Application.Conta;
using SoundCloudClone.API.Request;
using SoundCloudClone.Domain.Transacao;
using SoundCloudClone.API.Response;
using SoundCloudClone.Domain.Conta;

namespace SoundCloudClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult Criar(CriarUsuarioRequest request)
        {
            if(ModelState.IsValid == false) return BadRequest();

            Cartao cartao = new Cartao()
            {
                Limite = request.Cartao.Limite,
                Ativo = request.Cartao.Ativo,
                Numero = request.Cartao.Numero
            };

            var usuarioCriado = this.usuarioService.CriarConta(request.Nome, request.PlanoId, cartao);
            UsuarioResponse response = UsuarioParaResponse(usuarioCriado);

            return Created($"/usuario/{response.Id}", response);
        }

        private UsuarioResponse UsuarioParaResponse(Usuario usuarioCriado)
        {
            var response = new UsuarioResponse()
            {
                Id = usuarioCriado.Id,
                Nome = usuarioCriado.Nome,
                PlanoId = usuarioCriado.Assinaturas.FirstOrDefault(plano => plano.Ativo).Plano.Id
            };

            foreach (var item in usuarioCriado.Playlists)
            {
                var playlistResponse = new PlaylistResponse();
                playlistResponse.Id = item.Id;
                playlistResponse.Nome = item.Nome;
                response.Playlists.Add(playlistResponse);

                foreach (var musica in item.Musicas)
                {
                    playlistResponse.Musica.Add(new MusicaResponse()
                    {
                        Duracao = musica.Duracao,
                        Nome = musica.Nome,
                        Id = musica.Id
                    });
                }
            }
            return response;
        }

        [HttpGet("{id}")]
        public IActionResult ObterUsuario(Guid id)
        {
            var usuario = this.usuarioService.ObterUsuario(id);

            if (usuario == null) return NotFound();

            var response = UsuarioParaResponse(usuario);

            return Ok(response);
        }

        [HttpPost("{id}/favoritar/{idMusica}")]
        public IActionResult FavoritarMusica(Guid id, Guid idMusica)
        {
            this.usuarioService.FavoritarMusica(id, idMusica);

            var usuario = this.usuarioService.ObterUsuario(id);

            var response = UsuarioParaResponse(usuario);

            return Ok(response);
        }

        [HttpPost("{id}/desfavoritar/{idMusica}")]
        public IActionResult DesfavoritarMusica(Guid id, Guid idMusica)
        {
            this.usuarioService.DesfavoritarMusica(id, idMusica);

            var usuario = this.usuarioService.ObterUsuario(id);

            var response = UsuarioParaResponse(usuario);

            return Ok(response);
        }
    }
}

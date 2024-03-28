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
                
            }
        }
    }
}

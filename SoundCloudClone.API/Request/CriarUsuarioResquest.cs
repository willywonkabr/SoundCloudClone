using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.API.Request
{
    public class CriarUsuarioRequest
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public Guid PlanoId { get; set; }

        [Required]
        public CartaoRequest Cartao { get; set; }
    }

    public class CartaoRequest
    {
        public Boolean Ativo {  get; set; }
        public Decimal Limite { get; set; }
        public String Numero { get; set; }
    }
}

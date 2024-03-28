using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.Domain.Transacao
{
    public class Transacao
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public Decimal Valor { get; set; }
        public String Vendedor { get; set; }
        public String Descricao { get; set; }
    }
}

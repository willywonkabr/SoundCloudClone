using SoundCloudClone.Domain.Transacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.Repository.Transacao
{
    public class PlanoRepository
    {
        private SoundCloudContext soundCloudContext;
        public PlanoRepository(SoundCloudContext soundCloudContext)
        {
            this.soundCloudContext = soundCloudContext;
        }

        public Plano ObterPlanoById(Guid planoId)
        {
            return new Plano()
            {
                Id = new Guid("6a324c2b-1ba9-4d84-a0e7-8d6d0cc2c6e7"),
                Nome = "Plano Família",
                Descricao = "Plano para toda a família sem anúncios.",
                Valor = 29.99M
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SoundCloudClone.Domain.Conta;
using SoundCloudClone.Domain.Transacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.Repository
{
    public class SoundCloudContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Plano> Planos { get; set; }

        public SoundCloudContext(DbContextOptions<SoundCloudContext> options) : base(options)
        {

        }
    }
}

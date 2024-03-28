﻿using SoundCloudClone.Domain.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudClone.Repository.Conta
{
    public class UsuarioRepository
    {
        private static List<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public UsuarioRepository() { }

        public void Save(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            Usuarios.Add(usuario);
        }

        public Usuario ObterUsuario(Guid id)
        {
            return Usuarios.Where(usuario => usuario.Id == id).FirstOrDefault();
        }

        public void Update(Usuario usuario)
        {
            Usuarios.Remove(usuario);
            Usuarios.Add(usuario);
        }

        public void Remove(Usuario usuario)
        {
            Usuarios.Remove(usuario);
        }
    }
}

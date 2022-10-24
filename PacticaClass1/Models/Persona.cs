using System;
using System.Collections.Generic;

namespace PacticaClass1.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdPersona { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}

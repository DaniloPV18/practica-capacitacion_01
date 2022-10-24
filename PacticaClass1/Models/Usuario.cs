using System;
using System.Collections.Generic;

namespace PacticaClass1.Models
{
    public partial class Usuario
    {
        public int? IdPersona { get; set; }
        public int IdUsuario { get; set; }
        public string? Usuario1 { get; set; }
        public string? Clave { get; set; }
        public string? Estado { get; set; }

        public virtual Persona? IdPersonaNavigation { get; set; }
    }
}

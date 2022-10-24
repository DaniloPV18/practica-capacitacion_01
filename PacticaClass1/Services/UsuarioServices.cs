using Microsoft.EntityFrameworkCore;
using PacticaClass1.Interfaces;
using PacticaClass1.Models;
using System.Net;
using System.Security.Policy;
using System.Web.Mvc;

namespace PacticaClass1.Services
{
    public class UsuarioServices : IUsuario
    {
        private readonly DataContext _context;
        public UsuarioServices(DataContext context)
        {
            _context = context;
        }
        public async Task<dynamic> getAllUsers()
        {
            var dataContext = _context.Usuarios.Include(u => u.IdPersonaNavigation);
            return await dataContext.ToListAsync();
            //return await _context.Usuarios.ToListAsync();
        }

        public async Task<dynamic> getUser(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No existe el usuario con esa id.");
            }
            var usuario = await _context.Usuarios
                .Include(u => u.IdPersonaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return new
                {
                    Cadena = "No existe el usuario con esa id."
                };
            }
            return usuario;
        }

        public async Task<dynamic> insertUser(Usuario usuario)
        {
            _context.Add(usuario);
            return await _context.SaveChangesAsync();
        }

        public async Task<dynamic> updateUser(int id, Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}

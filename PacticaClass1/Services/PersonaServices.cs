using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PacticaClass1.Interfaces;
using PacticaClass1.Models;
using System.Net;
using System.Web.Mvc;

namespace PacticaClass1.Services
{
    public class PersonaServices : IPersona
    {
        private readonly DataContext _context;
        public PersonaServices(DataContext context)
        {
            _context = context;
        }
        public async Task<dynamic> getAllPersons()
        {
            var dataContext = _context.Personas.Include(u => u.Usuarios);
            return await dataContext.ToListAsync();
        }

        public async Task<dynamic> getPerson(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No existe la persona con esa id.");
            }
            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.IdPersona == id);
            if (persona == null)
            {
                return new
                {
                    Cadena = "No existe la persona con esa id."
                };
            }

            return persona;
        }

        public async Task<dynamic> insertPerson(Persona persona)
        {
            _context.Add(persona);
            return await _context.SaveChangesAsync();
        }

        public async Task<dynamic> updatePerson(int id, Persona persona)
        {
            throw new NotImplementedException();
        }
    }
}

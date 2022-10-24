using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PacticaClass1.Interfaces;
using PacticaClass1.Models;

namespace PacticaClass1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly DataContext _context;
        readonly private IUsuario _iusuario;

        public UsuariosController(DataContext context, IUsuario iusuario)
        {
            _context = context;
            _iusuario = iusuario;
        }

        // GET: Usuarios
        [HttpGet]
        [Route("listar")]
        public async Task<dynamic> Index()
        {
            try
            {
                dynamic res = await _iusuario.getAllUsers();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/user/{id?}")]
        public async Task<dynamic> Details(int? id)
        {            
            try
            {
                dynamic res = await _iusuario.getUser(id);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<dynamic> insertUser([Bind("IdPersona,IdUsuario,Usuario1,Clave,Estado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dynamic res = await _iusuario.insertUser(usuario);
                    return res;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Index();
        }

        // GET: Usuarios/Edit/5
        protected async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "IdPersona", usuario.IdPersona);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        protected async Task<IActionResult> Edit(int id, [Bind("IdPersona,IdUsuario,Usuario1,Clave,Estado")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "IdPersona", usuario.IdPersona);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        protected async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdPersonaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        protected async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'masterContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}

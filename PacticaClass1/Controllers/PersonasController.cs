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
    public class PersonasController : Controller
    {
        private readonly DataContext _context;
        private readonly IPersona _ipersona;

        public PersonasController(DataContext context, IPersona ipersona)
        {
            _context = context;
            _ipersona = ipersona;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<dynamic> Index()
        {
            try
            {
                dynamic res = await _ipersona.getAllPersons();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/persons/{id?}")]
        public async Task<dynamic> Details(int? id)
        {
            try
            {
                dynamic res = await _ipersona.getPerson(id);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<dynamic> insertPerson([Bind("IdPersona,Nombres,Apellidos,Correo,Direccion,Estado")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dynamic res = await _ipersona.insertPerson(persona);
                    return res;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Index();
        }

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult<Persona>> UpdateEmployee(int id, Persona persona)
        //{
        //    try
        //    {
        //        if (id != persona.IdPersona)
        //            return BadRequest("Employee ID mismatch");

        //        var employeeToUpdate = await employeeRepository.GetEmployee(id);

        //        if (employeeToUpdate == null)
        //            return NotFound($"Employee with Id = {id} not found");

        //        return await employeeRepository.UpdateEmployee(employee);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error updating data");
        //    }
        //}

        // GET: Personas/Edit/5
        protected async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [Route("/update/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("IdPersona,Nombres,Apellidos,Correo,Direccion,Estado")] Persona persona)
        {
            if (id != persona.IdPersona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.IdPersona))
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
            return View(persona);
        }

        // GET: Personas/Delete/5
        //protected async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Personas == null)
        //    {
        //        return NotFound();
        //    }

        //    var persona = await _context.Personas
        //        .FirstOrDefaultAsync(m => m.IdPersona == id);
        //    if (persona == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(persona);
        //}

        // POST: Personas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //protected async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Personas == null)
        //    {
        //        return Problem("Entity set 'masterContext.Personas'  is null.");
        //    }
        //    var persona = await _context.Personas.FindAsync(id);
        //    if (persona != null)
        //    {
        //        _context.Personas.Remove(persona);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.IdPersona == id);
        }
    }
}

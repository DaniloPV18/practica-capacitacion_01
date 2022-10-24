using PacticaClass1.Models;

namespace PacticaClass1.Interfaces
{
    public interface IPersona
    {
        Task<dynamic> getAllPersons();
        Task<dynamic> getPerson(int? id);
        Task<dynamic> insertPerson(Persona persona);
        Task<dynamic> updatePerson(int id, Persona persona);
    }
}

using PacticaClass1.Models;

namespace PacticaClass1.Interfaces
{
    public interface IUsuario
    {
        Task<dynamic> getAllUsers();
        Task<dynamic> getUser (int? id);
        Task<dynamic> insertUser(Usuario usuario);
        Task<dynamic> updateUser(int id, Usuario usuario);
    }
}

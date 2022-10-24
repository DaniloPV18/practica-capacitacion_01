using Newtonsoft.Json;
using PacticaClass1.Interfaces;
using System.Text.Json.Serialization;

namespace PacticaClass1.Services
{
    public class PruebaServices : PruebaInterface
    {
        public async Task<dynamic> Get()
        {
			try
			{
				dynamic res = new
				{
					Nombre = "Arturo",
					Apellido = "delgado",
					Edad = "27"
				};
                return await Task.FromResult(res);
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}

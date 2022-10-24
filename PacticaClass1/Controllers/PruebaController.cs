using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PacticaClass1.Interfaces;

namespace PacticaClass1.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PruebaController : ControllerBase
    {
        readonly private PruebaInterface pruebaInterface;

        public PruebaController(PruebaInterface _pruebaInterface)
        {
            this.pruebaInterface = _pruebaInterface;
        }


        [HttpGet(Name = "getprueba")]

        public async Task<dynamic> Get()
        {
            try
            {
                dynamic res = await pruebaInterface.Get();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

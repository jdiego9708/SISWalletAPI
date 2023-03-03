using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;

namespace SISWallet.API.Controllers
{
    
    [Route("api/")]
    [ApiController]
    public class CobrosController : ControllerBase
    {
        private readonly ILogger<CobrosController> logger;
        private ICobrosServicio ICobrosServicio { get; set; }
        public CobrosController(ILogger<CobrosController> logger,
            ICobrosServicio ICobrosServicio)
        {
            this.logger = logger;
            this.ICobrosServicio = ICobrosServicio;
        }

        [HttpPost]
        [Route("BuscarCobros")]
        public IActionResult BuscarCobros(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar cobros");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar cobros");
                    throw new Exception("Sin información de buscar cobros");
                }
                else
                {
                    RespuestaServicioModel rpta = this.ICobrosServicio.BuscarCobros(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar cobros correcto");
                        return Ok(rpta.Respuesta);
                    }
                    else
                    {
                        return BadRequest(rpta.Respuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error buscar cobros", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}

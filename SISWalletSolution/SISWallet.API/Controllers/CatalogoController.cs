using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Servicios.Interfaces;

namespace SISWallet.API.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ILogger<CatalogoController> logger;
        private ICatalogoServicio ICatalogoServicio { get; set; }
        public CatalogoController(ILogger<CatalogoController> logger,
            ICatalogoServicio ICatalogoServicio)
        {
            this.logger = logger;
            this.ICatalogoServicio = ICatalogoServicio;
        }

        [HttpPost]
        [Route("BuscarCatalogo")]
        public IActionResult BuscarCatalogo(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar catalogo");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar catalogo");
                    throw new Exception("Sin información de buscar catalogo");
                }
                else
                {
                    RespuestaServicioModel rpta = this.ICatalogoServicio.BuscarCatalogo(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar catalogo correcto");
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
                logger.LogError("Error buscar catalogo", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}

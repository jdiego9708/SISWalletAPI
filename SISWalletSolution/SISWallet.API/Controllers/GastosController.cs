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
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly ILogger<GastosController> logger;
        private IGastosServicio IGastosServicio { get; set; }
        public GastosController(ILogger<GastosController> logger,
            IGastosServicio IGastosServicio)
        {
            this.logger = logger;
            this.IGastosServicio = IGastosServicio;
        }

        [HttpPost]
        [Route("InsertarGasto")]
        public IActionResult InsertarGasto(JObject gastoJson)
        {
            try
            {
                logger.LogInformation("Inicio de nuevo gasto");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                Gastos gastoModel = gastoJson.ToObject<Gastos>();

                if (gastoModel == null)
                {
                    logger.LogInformation("Sin información de nuevo gasto");
                    throw new Exception("Sin información de nuevo gasto");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IGastosServicio.InsertarGasto(gastoModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Nuevo gasto correcto Id gasto: {gastoModel.Id_gasto}");
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
                logger.LogError("Error nuevo gasto", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarGastos")]
        public IActionResult BuscarGastos(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar gastos");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar gastos");
                    throw new Exception("Sin información de buscar gastos");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IGastosServicio.BuscarGastos(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar gastos correcto");
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
                logger.LogError("Error buscar gastos", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarTipoGastos")]
        public IActionResult BuscarTipoGastos(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar tipo gastos");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar tipo gastos");
                    throw new Exception("Sin información de buscar tipo gastos");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IGastosServicio.BuscarTipoGastos(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar tipo gastos correcto");
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
                logger.LogError("Error buscar tipo gastos", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}

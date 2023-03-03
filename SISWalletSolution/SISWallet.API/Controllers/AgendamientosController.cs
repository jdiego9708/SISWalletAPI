using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Servicios.Interfaces;

namespace SISWallet.API.Controllers
{
    
    [Route("api/")]
    [ApiController]
    public class AgendamientosController : ControllerBase
    {
        private readonly ILogger<AgendamientosController> logger;
        private IAgendamientosServicio IAgendamientosServicio { get; set; }
        public AgendamientosController(ILogger<AgendamientosController> logger,
            IAgendamientosServicio IAgendamientosServicio)
        {
            this.logger = logger;
            this.IAgendamientosServicio = IAgendamientosServicio;
        }

        [HttpPost]
        [Route("SincronizarFilas")]
        public IActionResult SincronizarFilas(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de SincronizarFilas");

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de SincronizarFilas");
                    throw new Exception("Sin información de SincronizarFilas");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IAgendamientosServicio.SincronizarFilas(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"SincronizarFilas correcta");
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
                logger.LogError("Error SincronizarFilas de agendamientos", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarAgendamientos")]
        public IActionResult BuscarAgendamientos(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de Busqueda de agendamientos");

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de busquedaModel");
                    throw new Exception("Sin información de busquedaModel");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IAgendamientosServicio.BuscarAgendamientos(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Busqueda correcta");
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
                logger.LogError("Error busqueda de agendamientos", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("PagarCuota")]
        public IActionResult PagarCuota(JObject pagarJson)
        {
            try
            {
                logger.LogInformation("Inicio de pagar cuota");

                PagarCuotaBindingModel pagarCuotaModel = pagarJson.ToObject<PagarCuotaBindingModel>();

                if (pagarCuotaModel == null)
                {
                    logger.LogInformation("Sin información de pagarCuotaModel");
                    throw new Exception("Sin información de pagarCuotaModel");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IAgendamientosServicio.PagarCuota(pagarCuotaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Busqueda correcta");
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
                logger.LogError("Error en pagar cuota", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AplazarCuota")]
        public IActionResult AplazarCuota(JObject pagarJson)
        {
            try
            {
                logger.LogInformation("Inicio de pagar cuota");

                PagarCuotaBindingModel pagarCuotaModel = pagarJson.ToObject<PagarCuotaBindingModel>();

                if (pagarCuotaModel == null)
                {
                    logger.LogInformation("Sin información de pagarCuotaModel");
                    throw new Exception("Sin información de pagarCuotaModel");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IAgendamientosServicio.AplazarCuota(pagarCuotaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Busqueda correcta");
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
                logger.LogError("Error en aplazar cuotas", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ActualizarOrdenAgendamiento")]
        public IActionResult ActualizarOrdenAgendamiento(JArray agJson)
        {
            try
            {
                logger.LogInformation("Inicio de actualizar orden");

                List<BusquedaBindingModel> busquedas = agJson.ToObject<List<BusquedaBindingModel>>();

                if (busquedas == null)
                {
                    logger.LogInformation("Sin información de busquedas");
                    throw new Exception("Sin información de busquedas");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IAgendamientosServicio.ActualizarOrdenAgendamiento(busquedas);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Busqueda correcta orden");
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
                logger.LogError("Error en actualizar orden", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ReingresarCuota")]
        public IActionResult ReingresarCuota(JObject agJson)
        {
            try
            {
                logger.LogInformation("Inicio de ReingresarCuota");

                CuotaMalaBindingModel cuota = agJson.ToObject<CuotaMalaBindingModel>();

                if (cuota == null)
                {
                    logger.LogInformation("Sin información de cuota");
                    throw new Exception("Sin información de cuota");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IAgendamientosServicio.ReingresarCuota(cuota);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Busqueda correcta ReingresarCuota");
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
                logger.LogError("Error en actualizar ReingresarCuota", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}

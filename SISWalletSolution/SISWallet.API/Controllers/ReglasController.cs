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
    public class ReglasController : ControllerBase
    {
        private readonly ILogger<ReglasController> logger;
        private IReglasServicio IReglasServicio { get; set; }
        public ReglasController(ILogger<ReglasController> logger,
            IReglasServicio IReglasServicio)
        {
            this.logger = logger;
            this.IReglasServicio = IReglasServicio;
        }

        [HttpPost]
        [Route("InsertarRegla")]
        public IActionResult InsertarRegla(JObject reglaJson)
        {
            try
            {
                logger.LogInformation("Inicio de nueva regla");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                Reglas reglaModel = reglaJson.ToObject<Reglas>();

                if (reglaModel == null)
                {
                    logger.LogInformation("Sin información de nueva regla");
                    throw new Exception("Sin información de nueva regla");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IReglasServicio.InsertarRegla(reglaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Nueva regla correcto Id regla: {reglaModel.Id_regla}");
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
                logger.LogError("Error nueva regla", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("EditarRegla")]
        public IActionResult EditarRegla(JObject reglaJson)
        {
            try
            {
                logger.LogInformation("Inicio de editar regla");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                Reglas reglaModel = reglaJson.ToObject<Reglas>();

                if (reglaModel == null)
                {
                    logger.LogInformation("Sin información de editar regla");
                    throw new Exception("Sin información de editar regla");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IReglasServicio.EditarRegla(reglaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"editar regla correcto Id regla: {reglaModel.Id_regla}");
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
                logger.LogError("Error editar regla", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarReglas")]
        public IActionResult BuscarReglas(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar reglas");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar reglas");
                    throw new Exception("Sin información de buscar reglas");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IReglasServicio.BuscarReglas(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar reglas correcto");
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
                logger.LogError("Error buscar reglas", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}

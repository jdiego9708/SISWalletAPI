using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;

namespace SISWallet.API.Controllers
{
    
    [Route("api/")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly ILogger<SolicitudesController> logger;
        private ISolicitudesServicio ISolicitudesServicio { get; set; }
        public SolicitudesController(ILogger<SolicitudesController> logger,
            ISolicitudesServicio ISolicitudesServicio)
        {
            this.logger = logger;
            this.ISolicitudesServicio = ISolicitudesServicio;
        }

        [HttpPost]
        [Route("InsertarSolicitud")]
        public IActionResult InsertarSolicitud(JObject solicitudJson)
        {
            try
            {
                logger.LogInformation("Inicio de nuevo solicitud");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                Solicitudes solicitudModel = solicitudJson.ToObject<Solicitudes>();

                if (solicitudModel == null)
                {
                    logger.LogInformation("Sin información de nuevo solicitud");
                    throw new Exception("Sin información de nuevo solicitud");
                }
                else
                {
                    RespuestaServicioModel rpta = this.ISolicitudesServicio.InsertarSolicitud(solicitudModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Nuevo solicitud correcto Id solicitud: {solicitudModel.Id_solicitud}");
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
                logger.LogError("Error nuevo solicitud", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarSolicitudes")]
        public IActionResult BuscarSolicitudes(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar solicitudes");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar solicitudes");
                    throw new Exception("Sin información de buscar solicitudes");
                }
                else
                {
                    RespuestaServicioModel rpta = this.ISolicitudesServicio.BuscarSolicitudes(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar solicitudes correcto");
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
                logger.LogError("Error buscar solicitudes", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}

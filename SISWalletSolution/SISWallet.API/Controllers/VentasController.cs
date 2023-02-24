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
    public class VentasController : ControllerBase
    {
        private readonly ILogger<VentasController> logger;
        private IVentasServicio IVentasServicio { get; set; }
        public VentasController(ILogger<VentasController> logger,
            IVentasServicio IVentasServicio)
        {
            this.logger = logger;
            this.IVentasServicio = IVentasServicio;
        }

        [HttpPost]
        [Route("BuscarEstadisticasDiarias")]
        public IActionResult BuscarEstadisticasDiarias(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de Busqueda de estadisticas");

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de busquedaModel");
                    throw new Exception("Sin información de busquedaModel");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IVentasServicio.BuscarEstadisticasDiarias(busquedaModel);
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
        [Route("BuscarTurnos")]
        public IActionResult BuscarTurnos(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de Busqueda de estadisticas");

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de busquedaModel");
                    throw new Exception("Sin información de busquedaModel");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IVentasServicio.BuscarTurnos(busquedaModel);
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
        [Route("BuscarVentas")]
        public IActionResult BuscarVentas(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de Busqueda de ventas");

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de busquedaModel");
                    throw new Exception("Sin información de busquedaModel");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IVentasServicio.BuscarVentas(busquedaModel);
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
        [Route("BuscarVentasDt")]
        public IActionResult BuscarVentasDt(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de Busqueda de ventas");

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de busquedaModel");
                    throw new Exception("Sin información de busquedaModel");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IVentasServicio.BuscarVentasDt(busquedaModel);
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
        [Route("RenovarVenta")]
        public IActionResult RenovarVenta(JObject clienteJson)
        {
            try
            {
                logger.LogInformation("Inicio de renovar venta");

                ClienteBindingModel clienteModel = clienteJson.ToObject<ClienteBindingModel>();

                if (clienteModel == null)
                {
                    logger.LogInformation("Sin información de renovar venta");
                    throw new Exception("Sin información de renovar venta");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IVentasServicio.RenovarVenta(clienteModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"renovar venta correcta");
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
                logger.LogError("Error renovar venta", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}

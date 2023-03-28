using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;

namespace SISWallet.API.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<GastosController> logger;
        private IProductosServicio IProductosServicio { get; set; }
        public ProductosController(ILogger<GastosController> logger,
            IProductosServicio IProductosServicio)
        {
            this.logger = logger;
            this.IProductosServicio = IProductosServicio;
        }

        [HttpPost]
        [Route("InsertarProducto")]
        public IActionResult InsertarProducto(JObject productoson)
        {
            try
            {
                logger.LogInformation("Inicio de nuevo producto");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                InsertarProductoBindingModel productoModel = productoson.ToObject<InsertarProductoBindingModel>();

                if (productoModel == null)
                {
                    logger.LogInformation("Sin información de nuevo producto");
                    throw new Exception("Sin información de nuevo producto");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IProductosServicio.InsertarProducto(productoModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Nuevo producto correcto Id producto: {productoModel.Producto.Id_producto}");
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
                logger.LogError("Error nuevo producto", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarProductos")]
        public IActionResult BuscarProductos(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar productos");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar productos");
                    throw new Exception("Sin información de buscar productos");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IProductosServicio.BuscarProductos(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar productos correcto");
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
                logger.LogError("Error buscar productos", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarProductosDt")]
        public IActionResult BuscarProductosDt(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar productos dt");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar productos dt");
                    throw new Exception("Sin información de buscar productos dt");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IProductosServicio.BuscarProductosDt(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar productos dt correcto");
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
                logger.LogError("Error buscar productos dt", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}

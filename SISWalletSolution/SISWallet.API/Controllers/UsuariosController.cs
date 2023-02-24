using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Servicios.Interfaces;

namespace SISWallet.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> logger;
        private IUsuariosServicio IUsuariosServicio { get; set; }
        public UsuariosController(ILogger<UsuariosController> logger,
            IUsuariosServicio IUsuariosServicio)
        {
            this.logger = logger;
            this.IUsuariosServicio = IUsuariosServicio;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(JObject loginJson)
        {
            try
            {
                logger.LogInformation("Inicio de Login");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                LoginModel loginModel = loginJson.ToObject<LoginModel>();

                if (loginModel == null)
                {
                    logger.LogInformation("Sin información de Login");
                    throw new Exception("Sin información de Login");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IUsuariosServicio.ProcesarLogin(loginModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Login correcto {loginModel.Usuario}");
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
                logger.LogError("Error iniciando sesión", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("NuevoCliente")]
        public IActionResult NuevoCliente(JObject clienteJson)
        {
            try
            {
                logger.LogInformation("Inicio de nuevo cliente");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                ClienteBindingModel clienteModel = clienteJson.ToObject<ClienteBindingModel>();

                if (clienteModel == null)
                {
                    logger.LogInformation("Sin información de nuevo cliente");
                    throw new Exception("Sin información de nuevo cliente");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IUsuariosServicio.NuevoCliente(clienteModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Nuevo cliente correcto Id usuario: {clienteModel.Usuario.Id_usuario}");
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
                logger.LogError("Error nuevo cliente", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarArchivos")]
        public IActionResult BuscarArchivos(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar archivos");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar archivos");
                    throw new Exception("Sin información de buscar archivos");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IUsuariosServicio.BuscarArchivos(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar archivos correcto");
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
                logger.LogError("Error buscar archivos", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertarArchivos")]
        public IActionResult InsertarArchivos(JArray busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de insertar archivos");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                List<Rutas_archivos> rutas = busquedaJson.ToObject<List<Rutas_archivos>>();

                if (rutas == null)
                {
                    logger.LogInformation("Sin información de insertar archivos");
                    throw new Exception("Sin información de insertar archivos");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IUsuariosServicio.InsertarArchivos(rutas);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Insertar archivos correcto");
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
                logger.LogError("Error insertar archivos", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SincronizarClientes")]
        public IActionResult SincronizarClientes(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de SincronizarClientes");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de SincronizarClientes");
                    throw new Exception("Sin información de SincronizarClientes");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IUsuariosServicio.SincronizarClientes(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"SincronizarClientes correcto");
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
                logger.LogError("Error SincronizarClientes", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}

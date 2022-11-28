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
    }
}

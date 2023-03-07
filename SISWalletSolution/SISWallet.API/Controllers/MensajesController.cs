using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionTwilio;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;

namespace SISWallet.API.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        private readonly ILogger<MensajesController> logger;
        private IMensajesServicio IMensajesServicio { get; set; }
        public MensajesController(ILogger<MensajesController> logger,
            IMensajesServicio IMensajesServicio)
        {
            this.logger = logger;
            this.IMensajesServicio = IMensajesServicio;
        }

        [HttpPost]
        [Route("SendMessage")]
        public IActionResult SendMessage(JObject messageJson)
        {
            try
            {
                logger.LogInformation("Inicio de SendMessage");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                TwilioMessageBindingModel mensajeModel = messageJson.ToObject<TwilioMessageBindingModel>();

                if (mensajeModel == null)
                {
                    logger.LogInformation("Sin información de SendMessage");
                    throw new Exception("Sin información de SendMessage");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IMensajesServicio.SendMessageTwilio(mensajeModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Send message correcto Id message: {mensajeModel.Id}");
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
                logger.LogError("Error send message", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}

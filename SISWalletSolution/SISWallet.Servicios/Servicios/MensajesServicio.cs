using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionTwilio;
using SISWallet.Servicios.Interfaces;
using Twilio.Rest.Api.V2010.Account;

namespace SISWallet.Servicios.Servicios
{
    public class MensajesServicio : IMensajesServicio
    {
        private readonly TwilioConfiguration TwilioConfiguration;
        public MensajesServicio(IConfiguration IConfiguration)
        {
            var settings = IConfiguration.GetSection("TwilioConfiguration");
            this.TwilioConfiguration = settings.Get<TwilioConfiguration>();
        }
        public void SendMessageTwilio(string body, string recipient_number)
        {
            MessageResource.Create(
                   body: body,
                   from: new Twilio.Types.PhoneNumber(this.TwilioConfiguration.TwilioPhoneNumber),
                   to: new Twilio.Types.PhoneNumber(recipient_number)
               );
        }
        public RespuestaServicioModel SendMessageTwilio(TwilioMessageBindingModel message)
        {

            RespuestaServicioModel respuesta = new();
            try
            {
                var messageResponse = MessageResource.Create(
                                   body: message.Body,
                                   from: new Twilio.Types.PhoneNumber(this.TwilioConfiguration.TwilioPhoneNumber),
                                   to: new Twilio.Types.PhoneNumber(message.To)
                               );

                message.Id = messageResponse.Sid;

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(message);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
    }
}

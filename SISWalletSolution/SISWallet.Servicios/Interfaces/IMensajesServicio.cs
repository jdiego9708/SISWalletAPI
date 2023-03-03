using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionTwilio;

namespace SISWallet.Servicios.Interfaces
{
    public interface IMensajesServicio
    {
        void SendMessageTwilio(string body, string recipient_number);
        RespuestaServicioModel SendMessageTwilio(TwilioMessageBindingModel message);
    }
}

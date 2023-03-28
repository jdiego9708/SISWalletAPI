using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.Servicios.Interfaces
{
    public interface INotificationService
    {
        Task<RespuestaServicioModel> BuscarRespuestaChatGPT(BusquedaBindingModel busqueda);
        Task SendNotification(string token);
        RespuestaServicioModel BuscarUsuariosFirebase(BusquedaBindingModel busqueda);
        RespuestaServicioModel InsertarUsuariosFirebase(Usuarios_firebase usuario_firebase);
    }
}

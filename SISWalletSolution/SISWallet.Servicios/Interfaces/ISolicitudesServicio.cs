using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;

namespace SISWallet.Servicios.Interfaces
{
    public interface ISolicitudesServicio
    {
        RespuestaServicioModel InsertarSolicitud(Solicitudes solicitud);
        RespuestaServicioModel BuscarSolicitudes(BusquedaBindingModel busqueda);
    }
}

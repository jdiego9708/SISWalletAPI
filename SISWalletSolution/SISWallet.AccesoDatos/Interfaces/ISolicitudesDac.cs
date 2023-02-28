using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.Models;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface ISolicitudesDac
    {
        Task<(DataTable dt, string rpta)> BuscarTipoSolicitudes(BusquedaBindingModel busqueda);
        Task<string> InsertarSolicitud(Solicitudes solicitud);
        Task<(DataTable dt, string rpta)> BuscarSolicitudes(BusquedaBindingModel busqueda);
    }
}

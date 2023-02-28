using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;

namespace SISWallet.Servicios.Interfaces
{
    public interface IVentasServicio
    {
        RespuestaServicioModel CerrarTurnos(Turnos turno);
        RespuestaServicioModel BuscarTurnos(BusquedaBindingModel busqueda);
        RespuestaServicioModel BuscarVentasDt(BusquedaBindingModel busqueda);
        RespuestaServicioModel RenovarVenta(ClienteBindingModel cliente);
        RespuestaServicioModel BuscarEstadisticasDiarias(BusquedaBindingModel busqueda);
        RespuestaServicioModel BuscarVentas(BusquedaBindingModel busqueda);
    }
}

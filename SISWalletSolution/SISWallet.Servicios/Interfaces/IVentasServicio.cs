using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.Servicios.Interfaces
{
    public interface IVentasServicio
    {
        RespuestaServicioModel BuscarTurnos(BusquedaBindingModel busqueda);
        RespuestaServicioModel BuscarVentasDt(BusquedaBindingModel busqueda);
        RespuestaServicioModel RenovarVenta(ClienteBindingModel cliente);
        RespuestaServicioModel BuscarEstadisticasDiarias(BusquedaBindingModel busqueda);
        RespuestaServicioModel BuscarVentas(BusquedaBindingModel busqueda);
    }
}

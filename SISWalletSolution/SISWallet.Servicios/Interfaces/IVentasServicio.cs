using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.Servicios.Interfaces
{
    public interface IVentasServicio
    {
        RespuestaServicioModel BuscarEstadisticasDiarias(BusquedaBindingModel busqueda);
        RespuestaServicioModel BuscarVentas(BusquedaBindingModel busqueda);
    }
}

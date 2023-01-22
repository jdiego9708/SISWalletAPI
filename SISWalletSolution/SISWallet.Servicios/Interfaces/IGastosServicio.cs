using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;

namespace SISWallet.Servicios.Interfaces
{
    public interface IGastosServicio
    {
        RespuestaServicioModel InsertarGasto(Gastos gasto);
        RespuestaServicioModel BuscarGastos(BusquedaBindingModel busqueda);
    }
}

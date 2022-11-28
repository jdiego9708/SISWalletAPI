using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.Servicios.Interfaces
{
    public interface IAgendamientosServicio
    {
        RespuestaServicioModel AplazarCuota(PagarCuotaBindingModel cuota);
        RespuestaServicioModel PagarCuota(PagarCuotaBindingModel cuota);
        RespuestaServicioModel BuscarAgendamientos(BusquedaBindingModel busqueda);
    }
}

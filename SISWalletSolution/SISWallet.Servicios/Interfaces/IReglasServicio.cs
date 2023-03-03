using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;

namespace SISWallet.Servicios.Interfaces
{
    public interface IReglasServicio
    {
        RespuestaServicioModel InsertarRegla(Reglas regla); 
        RespuestaServicioModel EditarRegla(Reglas regla);
        RespuestaServicioModel BuscarReglas(BusquedaBindingModel busqueda);
    }
}

using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.Servicios.Interfaces
{
    public interface ICatalogoServicio
    {
        RespuestaServicioModel InsertarCatalogo(Catalogo catalogo);
        RespuestaServicioModel BuscarCatalogo(BusquedaBindingModel busqueda);
    }
}

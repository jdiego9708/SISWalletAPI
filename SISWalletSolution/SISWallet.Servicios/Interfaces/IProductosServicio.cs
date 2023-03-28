using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.Servicios.Interfaces
{
    public interface IProductosServicio
    {
        RespuestaServicioModel InsertarProducto(InsertarProductoBindingModel producto);
        RespuestaServicioModel BuscarProductos(BusquedaBindingModel busqueda);
        RespuestaServicioModel BuscarProductosDt(BusquedaBindingModel busqueda);
    }
}

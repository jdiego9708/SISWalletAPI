using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IPrecios_productosDac
    {
        Task<string> InsertarPrecioProducto(Precios_productos precioProducto);
        Task<string> EditarPrecioProducto(Precios_productos precioProducto);
        Task<(string rpta, DataTable dt)> BuscarPreciosProductos(BusquedaBindingModel busqueda);
    }
}

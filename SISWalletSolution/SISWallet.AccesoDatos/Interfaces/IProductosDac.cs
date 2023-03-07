using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IProductosDac
    {
        Task<string> InsertarProducto(Productos producto);
        Task<string> EditarProducto(Productos producto);
        Task<(string rpta, DataTable dt)> BuscarProductos(BusquedaBindingModel busqueda);
    }
}

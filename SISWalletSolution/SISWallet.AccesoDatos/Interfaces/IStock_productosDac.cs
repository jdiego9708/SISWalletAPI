using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IStock_productosDac
    {
        Task<string> InsertarStockProducto(Stock_producto stockProducto);
        Task<string> EditarStockProducto(Stock_producto stockProducto);
        Task<(string rpta, DataTable dt)> BuscarStockProductos(BusquedaBindingModel busqueda);
    }
}

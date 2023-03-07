using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IPedidos_proveedorDac
    {
        Task<string> InsertarPedidoProveedor(Pedidos_proveedor pedidoProveedor);
        Task<string> EditarPedidoProveedor(Pedidos_proveedor pedidoProveedor);
        Task<(string rpta, DataTable dt)> BuscarProveedores(BusquedaBindingModel busqueda);
    }
}

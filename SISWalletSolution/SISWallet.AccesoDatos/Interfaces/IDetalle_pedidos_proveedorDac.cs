using SISWallet.Entidades.Modelos;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IDetalle_pedidos_proveedorDac
    {
        Task<string> InsertarDetallePedidoProveedor(Detalle_pedido_proveedor detallePedidoProveedor);
    }
}

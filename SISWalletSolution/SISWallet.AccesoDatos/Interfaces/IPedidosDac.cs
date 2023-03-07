using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IPedidosDac
    {
        Task<string> InsertarPedidos(Pedidos pedido);
        Task<(string rpta, DataTable dt)> BuscarPedidos(BusquedaBindingModel busqueda);
    }
}

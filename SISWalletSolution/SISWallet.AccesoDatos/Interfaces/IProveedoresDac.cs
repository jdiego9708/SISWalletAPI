using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IProveedoresDac
    {
        Task<string> InsertarProveedor(Proveedores proveedor);
        Task<string> EditarProveedor(Proveedores proveedor);
        Task<(string rpta, DataTable dt)> BuscarProveedores(BusquedaBindingModel busqueda);
    }
}

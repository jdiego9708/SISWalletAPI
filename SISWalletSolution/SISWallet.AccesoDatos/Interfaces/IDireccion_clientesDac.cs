using SISWallet.Entidades.Models;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IDireccion_clientesDac
    {
        Task<string> InsertarDireccion(Direccion_clientes direccion);
    }
}

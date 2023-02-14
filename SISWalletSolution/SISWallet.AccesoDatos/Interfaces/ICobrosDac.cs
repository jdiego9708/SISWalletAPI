using SISWallet.Entidades.ModelosBindeo;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface ICobrosDac
    {
        Task<(DataTable dtCobros, string rpta)> BuscarCobros(BusquedaBindingModel busqueda);
    }
}

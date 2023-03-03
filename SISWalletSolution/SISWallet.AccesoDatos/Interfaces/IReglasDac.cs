using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.Models;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IReglasDac
    {
        Task<string> InsertarRegla(Reglas regla);
        Task<string> EditarRegla(Reglas regla);
        Task<(DataTable dt, string rpta)> BuscarReglas(BusquedaBindingModel busqueda);
    }
}

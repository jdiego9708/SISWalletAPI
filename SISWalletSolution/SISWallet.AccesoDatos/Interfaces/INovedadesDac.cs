using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface INovedadesDac
    {
        Task<string> InsertarNovedad(Novedades novedad);
        Task<string> EditarNovedad(Novedades novedad);
        Task<(string rpta, DataTable dt)> BuscarNovedades(BusquedaBindingModel busqueda);
    }
}

using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface ICatalogoDac
    {
        Task<string> InsertarCatalogo(Catalogo catalogo);
        Task<(string rpta, DataTable dt)> BuscarCatalogo(BusquedaBindingModel busqueda);
    }
}

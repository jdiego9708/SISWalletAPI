using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.Models;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IGastosDac
    {
        Task<string> InsertarGastos(Gastos gasto);
        Task<(DataTable dtGastos, string rpta)> BuscarGastos(BusquedaBindingModel busqueda);
        Task<(DataTable dtGastos, string rpta)> BuscarTipoGastos(string tipo_busqueda, string texto_busqueda);
    }
}

using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.Models;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IRutas_archivosDac
    {
        Task<string> InsertarRuta(Rutas_archivos ruta);
        Task<(DataTable dtRutas, string rpta)> BuscarRutas(string tipo_busqueda, string texto_busqueda);
    }
}

using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.Models;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IUsuariosDac
    {
        Task<string> InsertarUsuario(Usuarios usuario);
        Task<string> EditarUsuario(Usuarios usuario);
        Task<(DataTable dtUsuarios, string rpta)> BuscarUsuarios(string tipo_busqueda, string texto_busqueda);
        Task<(DataTable dtClientes, string rpta)> BuscarClientes(string tipo_busqueda, string texto_busqueda1,
            string texto_busqueda2);
        Task<(string rpta, LoginDataModel loginData)> Login(string usuario, string pass, string fecha);
    }
}

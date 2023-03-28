using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.Models;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IUsuariosDac
    {
        Task<(DataTable dt, string rpta)> BuscarUsuariosFirebase(string tipo_busqueda, string texto_busqueda);
        Task<string> InsertarUsuarioFirebase(Usuarios_firebase usuario);
        Task<string> InsertarUsuarioVenta(Usuarios_ventas usuarios);
        Task<string> InsertarUsuario(Usuarios usuario);
        Task<string> EditarUsuario(Usuarios usuario);
        Task<(DataTable dtUsuarios, string rpta)> BuscarUsuarios(string tipo_busqueda, string texto_busqueda);
        Task<(DataTable dtClientes, string rpta)> BuscarClientes(string tipo_busqueda, string texto_busqueda1,
            string texto_busqueda2);
        Task<(string rpta, LoginDataModel loginData)> Login(string usuario, string pass, string fecha);
    }
}

using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.Servicios.Interfaces
{
    public interface IUsuariosServicio
    {
        RespuestaServicioModel BuscarArchivos(BusquedaBindingModel busqueda);
        RespuestaServicioModel ProcesarLogin(LoginModel login);
        RespuestaServicioModel NuevoCliente(ClienteBindingModel cliente);
    }
}

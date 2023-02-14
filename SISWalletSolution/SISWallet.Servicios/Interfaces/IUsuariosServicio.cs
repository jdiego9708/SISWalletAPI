using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.Servicios.Interfaces
{
    public interface IUsuariosServicio
    {
        RespuestaServicioModel InsertarArchivos(List<Rutas_archivos> rutas);
        RespuestaServicioModel BuscarArchivos(BusquedaBindingModel busqueda);
        RespuestaServicioModel ProcesarLogin(LoginModel login);
        RespuestaServicioModel NuevoCliente(ClienteBindingModel cliente);
    }
}

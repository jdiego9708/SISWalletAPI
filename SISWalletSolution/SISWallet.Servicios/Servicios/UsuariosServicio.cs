using Newtonsoft.Json;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Servicios.Interfaces;
using System.Data;

namespace SISWallet.Servicios.Servicios
{
    public class UsuariosServicio : IUsuariosServicio
    {
        #region CONSTRUCTOR
        public IUsuariosDac UsuariosDac { get; set; }
        public UsuariosServicio(IUsuariosDac UsuariosDac)
        {
            this.UsuariosDac = UsuariosDac;
        }
        #endregion

        public RespuestaServicioModel ProcesarLogin(LoginModel login)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.UsuariosDac.Login(login.Usuario, login.Clave, login.Fecha.ToString("yyyy-MM-dd")).Result;

                string rpta = result.rpta;
                LoginDataModel loginData = result.loginData;

                if (loginData == null)
                    throw new Exception("Error obteniendo los datos de inicio de sesión");

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(loginData);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
    }
}

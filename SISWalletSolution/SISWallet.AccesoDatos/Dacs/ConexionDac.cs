using SISWallet.AccesoDatos.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.AccesoDatos.Dacs
{
    public class ConexionDac : IConexionDac
    {
        public IConfiguration Configuration { get; set; }
        public ConfiguracionJWT ConfiguracionJWT { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public ConfiguracionSISWallet ConfiguracionSISWallet { get; set; }
        public ConexionDac(IConfiguration Configuration)
        {
            this.Configuration = Configuration;

            var settings = this.Configuration.GetSection("ConnectionStrings");
            this.ConnectionStrings = settings.Get<ConnectionStrings>() ?? new ConnectionStrings();

            settings = this.Configuration.GetSection("ConfiguracionSISWallet");
            this.ConfiguracionSISWallet = settings.Get<ConfiguracionSISWallet>() ?? new ConfiguracionSISWallet();

            settings = this.Configuration.GetSection("ConfiguracionJWT");
            this.ConfiguracionJWT = settings.Get<ConfiguracionJWT>() ?? new ConfiguracionJWT();
        }
        public string Cn()
        {
            string connectionDefault = this.ConfiguracionSISWallet.BDPredeterminada;

            if (connectionDefault.Equals("ConexionBDDesarrollo"))
                return ConnectionStrings.ConexionBDDesarrollo;
            else if (connectionDefault.Equals("ConexionBDProduccion"))
                return ConnectionStrings.ConexionBDProduccion;
            else
                return ConnectionStrings.ConexionBDDesarrollo;
        }
    }
}

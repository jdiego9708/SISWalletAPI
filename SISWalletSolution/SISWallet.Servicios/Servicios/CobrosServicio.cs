using Newtonsoft.Json;
using SISWallet.AccesoDatos;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Helpers.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;
using System.Collections;
using System.Data;

namespace SISWallet.Servicios.Servicios
{
    public class CobrosServicio : ICobrosServicio
    {
        #region CONSTRUCTOR
        public ICobrosDac CobrosDac { get; set; }
        public IRutas_archivosDac Rutas_archivosDac { get; set; }
        public IBlobStorageService BlobStorageService { get; set; }
        public CobrosServicio(ICobrosDac CobrosDac, 
            IBlobStorageService BlobStorageService,
            IRutas_archivosDac Rutas_archivosDac)
        {
            this.CobrosDac = CobrosDac;
            this.BlobStorageService = BlobStorageService;
            this.Rutas_archivosDac = Rutas_archivosDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel BuscarCobros(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.CobrosDac.BuscarCobros(busqueda).Result;

                if (result.dtCobros == null)
                    throw new Exception("Error al buscar cobros");

                if (result.dtCobros.Rows.Count < 1)
                    throw new Exception("No se encontraron cobros");

                List<Cobros> cobros = (from DataRow row in result.dtCobros.Rows
                                       select new Cobros(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(cobros);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        #endregion
    }
}

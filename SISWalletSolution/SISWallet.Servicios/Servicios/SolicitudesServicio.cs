using Newtonsoft.Json;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Helpers.Interfaces;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;
using System.Data;

namespace SISWallet.Servicios.Servicios
{
    public class SolicitudesServicio : ISolicitudesServicio
    {
        #region CONSTRUCTOR
        public ISolicitudesDac ISolicitudesDac { get; set; }
        public IRutas_archivosDac Rutas_archivosDac { get; set; }
        public IBlobStorageService BlobStorageService { get; set; }
        public SolicitudesServicio(ISolicitudesDac ISolicitudesDac, 
            IBlobStorageService BlobStorageService,
            IRutas_archivosDac Rutas_archivosDac)
        {
            this.ISolicitudesDac = ISolicitudesDac;
            this.BlobStorageService = BlobStorageService;
            this.Rutas_archivosDac = Rutas_archivosDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel InsertarSolicitud(Solicitudes solicitud)
        {

            RespuestaServicioModel respuesta = new();
            try
            {
                //Insertar Usuario
                string rpta = this.ISolicitudesDac.InsertarSolicitud(solicitud).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando la solicitud, detalles: {rpta}");
                
                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(solicitud);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarSolicitudes(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var (dt, rpta) = this.ISolicitudesDac.BuscarSolicitudes(busqueda).Result;

                if (dt == null)
                    throw new Exception("Error al buscar solicitudes");

                if (dt.Rows.Count < 1)
                    throw new Exception("No se encontraron solicitudes");

                List<Solicitudes> solicitudes = (from DataRow row in dt.Rows
                                            select new Solicitudes(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(solicitudes);
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

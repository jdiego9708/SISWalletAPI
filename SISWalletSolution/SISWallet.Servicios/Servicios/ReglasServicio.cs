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
    public class ReglasServicio : IReglasServicio
    {
        #region CONSTRUCTOR
        public IReglasDac IReglasDac { get; set; }
        public IRutas_archivosDac Rutas_archivosDac { get; set; }
        public IBlobStorageService BlobStorageService { get; set; }
        public ReglasServicio(IReglasDac IReglasDac, 
            IBlobStorageService BlobStorageService,
            IRutas_archivosDac Rutas_archivosDac)
        {
            this.IReglasDac = IReglasDac;
            this.BlobStorageService = BlobStorageService;
            this.Rutas_archivosDac = Rutas_archivosDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel InsertarRegla(Reglas regla)
        {

            RespuestaServicioModel respuesta = new();
            try
            {
                //Insertar Usuario
                string rpta = this.IReglasDac.InsertarRegla(regla).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando la regla, detalles: {rpta}");
                
                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(regla);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel EditarRegla(Reglas regla)
        {

            RespuestaServicioModel respuesta = new();
            try
            {
                //Insertar Usuario
                string rpta = this.IReglasDac.EditarRegla(regla).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error editando la regla, detalles: {rpta}");

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(regla);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarReglas(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.IReglasDac.BuscarReglas(busqueda).Result;

                if (result.dt == null)
                    throw new Exception("Error al buscar reglas");

                if (result.dt.Rows.Count < 1)
                    throw new Exception("No se encontraron reglas");

                List<Reglas> reglas = (from DataRow row in result.dt.Rows
                                       select new Reglas(row)).ToList();
  
                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(reglas);
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

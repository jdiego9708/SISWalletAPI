using Newtonsoft.Json;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Helpers.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Servicios.Interfaces;
using System.Data;

namespace SISWallet.Servicios.Servicios
{
    public class CatalogoServicio : ICatalogoServicio
    {
        #region CONSTRUCTOR
        public ICatalogoDac ICatalogoDac { get; set; }
        public IRutas_archivosDac Rutas_archivosDac { get; set; }
        public IBlobStorageService BlobStorageService { get; set; }
        public CatalogoServicio(ICatalogoDac ICatalogoDac, 
            IBlobStorageService BlobStorageService,
            IRutas_archivosDac Rutas_archivosDac)
        {
            this.ICatalogoDac = ICatalogoDac;
            this.BlobStorageService = BlobStorageService;
            this.Rutas_archivosDac = Rutas_archivosDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel InsertarCatalogo(Catalogo catalogo)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                string rpta = this.ICatalogoDac.InsertarCatalogo(catalogo).Result;

                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el catalogo, detalles: {rpta}");

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(catalogo);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarCatalogo(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var (rpta, dt) = this.ICatalogoDac.BuscarCatalogo(busqueda).Result;

                if (dt == null)
                    throw new Exception("Error al buscar catalogos");

                if (dt.Rows.Count < 1)
                    throw new Exception("No se encontraron catalogos");

                List<Catalogo> catalogos = (from DataRow row in dt.Rows
                                            select new Catalogo(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(catalogos);
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

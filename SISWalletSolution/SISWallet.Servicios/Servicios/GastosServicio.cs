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
    public class GastosServicio : IGastosServicio
    {
        #region CONSTRUCTOR
        public IGastosDac GastosDac { get; set; }
        public IRutas_archivosDac Rutas_archivosDac { get; set; }
        public IBlobStorageService BlobStorageService { get; set; }
        public GastosServicio(IGastosDac GastosDac, 
            IBlobStorageService BlobStorageService,
            IRutas_archivosDac Rutas_archivosDac)
        {
            this.GastosDac = GastosDac;
            this.BlobStorageService = BlobStorageService;
            this.Rutas_archivosDac = Rutas_archivosDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel InsertarGasto(Gastos gasto)
        {

            RespuestaServicioModel respuesta = new();
            try
            {
                //Insertar Usuario
                string rpta = this.GastosDac.InsertarGastos(gasto).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el gasto, detalles: {rpta}");

                if (gasto.Imagenes != null)
                {
                    if (gasto.Imagenes.Count > 0)
                    {
                        int contador = 1;
                        foreach (string imagenbase64 in gasto.Imagenes)
                        {
                            byte[] imagenByte = Convert.FromBase64String(imagenbase64);

                            Stream stream = new MemoryStream(imagenByte);

                            BlobResponse response = this.BlobStorageService.SubirArchivoContainerBlobStorage(stream,
                                $"{gasto.Id_gasto}Imagen{contador}.png", "imagenesusuario");

                            if (!response.IsSuccess)
                                throw new Exception("Error guardando las imagenes en el blob");

                            string uri = Convert.ToString(response.Message);

                            if (string.IsNullOrEmpty(uri))
                                throw new Exception("Error con la URL devuelta");

                            DirectoryInfo info = new(uri);

                            Rutas_archivos ruta = new()
                            {
                                Id_usuario = gasto.Id_gasto,
                                Tipo_archivo = "GASTO COBRADOR",
                                Fecha_archivo = gasto.Fecha_gasto,
                                Hora_archivo = DateTime.Now.TimeOfDay,
                                Nombre_archivo = Path.GetFileName(info.FullName),
                                Ruta_archivo = uri,
                                Extension_archivo = info.Extension,
                            };

                            rpta = this.Rutas_archivosDac.InsertarRuta(ruta).Result;
                            if (!rpta.Equals("OK"))
                                throw new Exception("Error guardando las imágenes en la BD");

                            contador++;
                        }
                    }
                }
                
                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(gasto);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarGastos(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.GastosDac.BuscarGastos(busqueda).Result;

                if (result.dtGastos == null)
                    throw new Exception("Error al buscar gastos");

                if (result.dtGastos.Rows.Count < 1)
                    throw new Exception("No se encontraron gastos");

                List<Gastos> gastos = new();

                foreach (DataRow row in result.dtGastos.Rows)
                {
                    Gastos gasto = new(row);

                    var resultImages = this.Rutas_archivosDac.BuscarRutas("ID GASTO", gasto.Id_gasto.ToString()).Result;

                    if (resultImages.dtRutas == null)
                        throw new Exception("Error al buscar rutas");

                    if (resultImages.dtRutas.Rows.Count < 1)
                        throw new Exception("No se encontraron rutas");

                    var rutas = (from DataRow rowRuta in resultImages.dtRutas.Rows
                                           select new Rutas_archivos(rowRuta)).ToList();

                    if (gasto.ImagenesGuardadas == null)
                        gasto.ImagenesGuardadas = new();

                    gasto.ImagenesGuardadas.AddRange(rutas);

                    gastos.Add(gasto);
                }

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(gastos);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarTipoGastos(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.GastosDac.BuscarTipoGastos(busqueda.Tipo_busqueda, busqueda.Texto_busqueda1).Result;

                if (result.dtGastos == null)
                    throw new Exception("Error al buscar tipo gastos");

                if (result.dtGastos.Rows.Count < 1)
                    throw new Exception("No se encontraron tipo gastos");

                List<Tipo_gastos> tipos = (from DataRow row in result.dtGastos.Rows
                                       select new Tipo_gastos(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(tipos);
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

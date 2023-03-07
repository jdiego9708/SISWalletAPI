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
    public class ProductosServicio : IProductosServicio
    {
        #region CONSTRUCTOR
        public IProductosDac IProductosDac { get; set; }
        public IRutas_archivosDac Rutas_archivosDac { get; set; }
        public IBlobStorageService BlobStorageService { get; set; }
        public ProductosServicio(IProductosDac IProductosDac, 
            IBlobStorageService BlobStorageService,
            IRutas_archivosDac Rutas_archivosDac)
        {
            this.IProductosDac = IProductosDac;
            this.BlobStorageService = BlobStorageService;
            this.Rutas_archivosDac = Rutas_archivosDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel InsertarProducto(Productos producto)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                string rpta = this.IProductosDac.InsertarProducto(producto).Result;

                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el producto, detalles: {rpta}");

                if (producto.Imagenes != null)
                {
                    if (producto.Imagenes.Count > 0)
                    {
                        int contador = 1;
                        foreach (string imagenbase64 in producto.Imagenes)
                        {
                            byte[] imagenByte = Convert.FromBase64String(imagenbase64);

                            Stream stream = new MemoryStream(imagenByte);

                            BlobResponse response = this.BlobStorageService.SubirArchivoContainerBlobStorage(stream,
                                $"{producto.Id_producto}Imagen{contador}.png", "imagenesusuario");

                            if (!response.IsSuccess)
                                throw new Exception("Error guardando las imagenes en el blob");

                            string uri = Convert.ToString(response.Message);

                            if (string.IsNullOrEmpty(uri))
                                throw new Exception("Error con la URL devuelta");

                            DirectoryInfo info = new(uri);

                            Rutas_archivos ruta = new()
                            {
                                Id_usuario = producto.Id_producto,
                                Tipo_archivo = "IMAGEN PRODUCTO",
                                Fecha_archivo = producto.Fecha_ingreso,
                                Hora_archivo = producto.Fecha_ingreso.TimeOfDay,
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
                respuesta.Respuesta = JsonConvert.SerializeObject(producto);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarProductos(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var (rpta, dt) = this.IProductosDac.BuscarProductos(busqueda).Result;

                if (dt == null)
                    throw new Exception("Error al buscar productos");

                if (dt.Rows.Count < 1)
                    throw new Exception("No se encontraron productos");

                List<Productos> productos = (from DataRow row in dt.Rows
                                            select new Productos(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(productos);
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

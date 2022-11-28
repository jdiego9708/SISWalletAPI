using Newtonsoft.Json;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;
using System.Data;

namespace SISWallet.Servicios.Servicios
{
    public class VentasServicio : IVentasServicio
    {
        #region CONSTRUCTOR
        public IVentasDac IVentasDac { get; set; }
        public VentasServicio(IVentasDac IVentasDac)
        {
            this.IVentasDac = IVentasDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel BuscarVentas(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.IVentasDac.BuscarVentas(busqueda.Tipo_busqueda,
                    busqueda.Texto_busqueda1).Result;

                string rpta = result.rpta;

                DataTable dtVentas = result.dtVentas;

                if (dtVentas == null)
                    throw new Exception($"Error obteniendo ventas | {rpta}");

                List<Ventas> ventas = (from DataRow row in dtVentas.Rows
                                       select new Ventas(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(ventas);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarEstadisticasDiarias(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                if (string.IsNullOrEmpty(busqueda.Texto_busqueda1))
                    throw new Exception("No están los parametros de busqueda");

                if (!int.TryParse(busqueda.Texto_busqueda1, out int id_turno))
                    throw new Exception("El primer parámetro debe ser el Id_turno");

                if (string.IsNullOrEmpty(busqueda.Texto_busqueda2))
                    throw new Exception("El segundo parámetro debe ser la fecha");

                var result = this.IVentasDac.BuscarEstadisticasDiarias(busqueda.Texto_busqueda1,
                    busqueda.Texto_busqueda2).Result;

                string rpta = result.rpta;

                DataSet dsEstadistica = result.ds;

                if (dsEstadistica == null)
                    throw new Exception($"Error obteniendo estadisticas | {rpta}");

                DataTable dtTurno = dsEstadistica.Tables[0];

                Turnos turno = new(dtTurno.Rows[0]);

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(turno);
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

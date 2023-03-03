using Newtonsoft.Json;
using SISWallet.AccesoDatos;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;
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
        public IRutas_archivosDac Rutas_archivosDac { get; set; }
        public IUsuariosDac IUsuariosDac { get; set; }
        public IAgendamiento_cobrosDac IAgendamiento_cobrosDac { get; set; }
        public ITurnosDac ITurnosDac { get; set; }
        public VentasServicio(IVentasDac IVentasDac,
            IRutas_archivosDac Rutas_archivosDac,
            IUsuariosDac IUsuariosDac,
            IAgendamiento_cobrosDac IAgendamiento_cobrosDac,
            ITurnosDac ITurnosDac)
        {
            this.IVentasDac = IVentasDac;
            this.Rutas_archivosDac = Rutas_archivosDac;
            this.IUsuariosDac = IUsuariosDac;
            this.IAgendamiento_cobrosDac = IAgendamiento_cobrosDac;
            this.ITurnosDac = ITurnosDac;
        }
        #endregion

        #region MÉTODOS
        private bool Comprobaciones(ClienteBindingModel cliente, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (cliente == null)
                    throw new Exception("Verifique el cliente");

                if (cliente.Agendamiento == null)
                    throw new Exception("Verifique el agendamiento");

                if (cliente.Venta == null)
                    throw new Exception("Verifique la venta");

                if (cliente.Usuario == null)
                    throw new Exception("Verifique el usuario");

                if (cliente.Direccion_cliente == null)
                    throw new Exception("Verifique la dirección");

                if (cliente.Venta.Id_cliente == 0)
                    throw new Exception("Verifique la venta");

                if (cliente.Venta.Id_cobro == 0)
                    throw new Exception("Verifique el cobro");

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RespuestaServicioModel RenovarVenta(ClienteBindingModel cliente)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                if (!this.Comprobaciones(cliente, out string rpta))
                    throw new Exception(rpta);

                rpta = this.IVentasDac.InsertarVentas(cliente.Venta).Result;

                if (!rpta.Equals("OK"))
                    throw new Exception(rpta);

                //Insertar usuario venta
                rpta =
                    this.IUsuariosDac.InsertarUsuarioVenta(new Usuarios_ventas
                    {
                        Id_usuario = cliente.Usuario.Id_usuario,
                        Id_venta = cliente.Venta.Id_venta,
                    }).Result;

                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el usuario venta, detalles: {rpta}");

                cliente.Agendamiento.Id_venta = cliente.Venta.Id_venta;

                rpta =
                    this.IAgendamiento_cobrosDac.InsertarAgendamiento(cliente.Agendamiento).Result;

                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el agendamiento, detalles: {rpta}");

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(cliente);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarVentas(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.IVentasDac.BuscarVentas(busqueda).Result;

                string rpta = result.rpta;

                DataTable dtVentas = result.dtVentas;

                if (dtVentas == null)
                    throw new Exception($"Sin resultados| {rpta}");

                List<Ventas> ventas = (from DataRow row in dtVentas.Rows
                                       select new Ventas(row)).ToList();

                //foreach (DataRow row in dtVentas.Rows)
                //{
                //    Ventas venta = new(row);

                //    //if (venta.ImagenesGuardadas == null)
                //    //    venta.ImagenesGuardadas = new();

                //    ventas.Add(venta);

                //    //if (row.Table.Columns.Contains("Id_ruta_archivo"))
                //    //{
                //    //    string idruta = Convert.ToString(row["Id_ruta_archivo"]);

                //    //    if (!string.IsNullOrEmpty(idruta))
                //    //    {
                //    //        var resultImages = this.Rutas_archivosDac.BuscarRutas("ID USUARIO", venta.Id_cliente.ToString()).Result;

                //    //        if (resultImages.dtRutas == null)
                //    //            continue;

                //    //        if (resultImages.dtRutas.Rows.Count < 1)
                //    //            continue;

                //    //        var rutas = (from DataRow rowRuta in resultImages.dtRutas.Rows
                //    //                     select new Rutas_archivos(rowRuta)).ToList();

                //    //        venta.ImagenesGuardadas.AddRange(rutas);
                //    //    }
                //    //}                                         
                //}

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
        public RespuestaServicioModel BuscarTurnos(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.ITurnosDac.BuscarTurnos(busqueda).Result;

                string rpta = result.rpta;

                DataTable dtTurnos = result.dt;

                if (dtTurnos == null)
                    throw new Exception($"Error obteniendo turnos | {rpta}");

                List<Turnos> turnos = (from DataRow row in dtTurnos.Rows
                                       select new Turnos(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(turnos);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel CerrarTurnos(Turnos turno)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                string rpta = this.ITurnosDac.CerrarTurno(turno).Result;

                if (!rpta.Equals("OK"))
                    throw new Exception(rpta);

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
        public RespuestaServicioModel BuscarVentasDt(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.IVentasDac.BuscarVentas(busqueda).Result;

                string rpta = result.rpta;

                DataTable dtVentas = result.dtVentas;

                if (dtVentas == null)
                    throw new Exception($"Error obteniendo ventas | {rpta}");

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(dtVentas);
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

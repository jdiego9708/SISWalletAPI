using Newtonsoft.Json;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;
using StackExchange.Redis;
using System.Data;

namespace SISWallet.Servicios.Servicios
{
    public class AgendamientosServicio : IAgendamientosServicio
    {
        #region CONSTRUCTOR
        public IAgendamiento_cobrosDac IAgendamiento_cobrosDac { get; set; }
        public IVentasDac IVentasDac { get; set; }
        public AgendamientosServicio(IAgendamiento_cobrosDac IAgendamiento_cobrosDac,
            IVentasDac IVentasDac)
        {
            this.IAgendamiento_cobrosDac = IAgendamiento_cobrosDac;
            this.IVentasDac = IVentasDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel ActualizarOrdenAgendamiento(List<BusquedaBindingModel> busquedas)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                string rpta = "OK";

                foreach(BusquedaBindingModel busqueda in busquedas)
                {
                    if (string.IsNullOrEmpty(busqueda.Texto_busqueda1))
                        throw new Exception("El texto busqueda debe ser un id de agendamiento");

                    if (string.IsNullOrEmpty(busqueda.Texto_busqueda2))
                        throw new Exception("El texto busqueda debe ser un id de agendamiento");

                    int id_agendamiento = Convert.ToInt32(busqueda.Texto_busqueda1);
                    int orden = Convert.ToInt32(busqueda.Texto_busqueda2);

                    rpta = this.IAgendamiento_cobrosDac.ActualizarOrden(id_agendamiento, orden).Result;

                }

                if (rpta.Equals("OK"))
                {
                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(busquedas);
                }
                else
                {
                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(new
                    {
                        MensajeError = $"Error pagando la cuota | {rpta}"
                    });
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = JsonConvert.SerializeObject(new
                {
                    MensajeError = $"Error pagando la cuota | {ex.Message}"
                });
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarAgendamientos(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.IAgendamiento_cobrosDac.BuscarAgendamiento(busqueda.Tipo_busqueda, 
                    busqueda.Textos_busqueda.ToArray()).Result;

                string rpta = result.rpta;

                DataTable dtAgendamientos = result.dtAgendamientos;

                if (dtAgendamientos == null)
                    throw new Exception("Error buscando los agendamientos");

                if (dtAgendamientos.Rows.Count < 1)
                    throw new Exception("Error buscando los agendamientos");

                List<Agendamiento_cobros> agendamientos = (from DataRow row in dtAgendamientos.Rows
                                                           select new Agendamiento_cobros(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(agendamientos);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel PagarCuota(PagarCuotaBindingModel cuota)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                string rpta = this.IAgendamiento_cobrosDac.TerminarAgendamiento(cuota.Id_agendamiento, 
                    "TERMINADO", cuota.Valor_pagar, cuota.Saldo_restante).Result;

                if (rpta.Equals("OK"))
                {
                    if (cuota.Saldo_restante <= 0)
                    {
                        rpta = this.IVentasDac.CambiarEstadoVenta(cuota.Id_venta, "TERMINADO").Result;
                        if (!rpta.Equals("OK")) 
                        {
                            respuesta.Correcto = true;
                            respuesta.Respuesta = JsonConvert.SerializeObject(new
                            {
                                MensajeError = $"Error pagando la cuota en el momento de cambiar estado la venta| {rpta}"
                            });
                        }
                    }

                    rpta = this.IAgendamiento_cobrosDac.InsertarAgendamiento(cuota.AgendamientoNuevo).Result;

                    if (!rpta.Equals("OK"))
                    {
                        respuesta.Correcto = true;
                        respuesta.Respuesta = JsonConvert.SerializeObject(new
                        {
                            MensajeError = $"Error insertando el nuevo agendamiento | {rpta}"
                        });
                        return respuesta;
                    }

                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(cuota);
                }
                else
                {
                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(new 
                    { 
                        MensajeError = $"Error pagando la cuota | {rpta}" 
                    });
                }
                
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = JsonConvert.SerializeObject(new
                {
                    MensajeError = $"Error pagando la cuota | {ex.Message}"
                });
                return respuesta;
            }
        }
        public RespuestaServicioModel AplazarCuota(PagarCuotaBindingModel cuota)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                string rpta = 
                    this.IAgendamiento_cobrosDac.CambiarEstadoAgendamiento(cuota.Id_agendamiento,
                    "NO COBRADO").Result;

                if (rpta.Equals("OK"))
                {
                    rpta = this.IAgendamiento_cobrosDac.InsertarAgendamiento(cuota.AgendamientoNuevo).Result;

                    if (!rpta.Equals("OK"))
                    {
                        respuesta.Correcto = true;
                        respuesta.Respuesta = JsonConvert.SerializeObject(new
                        {
                            MensajeError = $"Error insertando el nuevo agendamiento | {rpta}"
                        });
                        return respuesta;
                    }

                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(cuota);
                }
                else
                {
                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(new
                    {
                        MensajeError = $"Error aplazando la cuota | {rpta}"
                    });
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = JsonConvert.SerializeObject(new
                {
                    MensajeError = $"Error aplazando la cuota | {ex.Message}"
                });
                return respuesta;
            }
        }
        public RespuestaServicioModel SincronizarFilas(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var (dtAgendamientos, rpta) = this.IAgendamiento_cobrosDac.BuscarAgendamiento("AGENDAMIENTOS CONTEO FILAS",
                    busqueda.Textos_busqueda.ToArray()).Result;

                if (dtAgendamientos == null)
                    throw new Exception("Error obteniendo el conteo filas");

                foreach(DataRow row in dtAgendamientos.Rows)
                {
                    int id_agendamiento = Convert.ToInt32(row["Id_agendamiento"]);
                    int orden = Convert.ToInt32(row["Orden"]);

                    rpta = this.IAgendamiento_cobrosDac.ActualizarOrden(id_agendamiento, orden).Result;

                    if (!rpta.Equals("OK"))
                        throw new Exception($"Error actualizando el orden de {id_agendamiento}");
                }

                if (rpta.Equals("OK"))
                {
                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(dtAgendamientos);
                }
                else
                {
                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(new
                    {
                        MensajeError = $"Error aplazando la cuota | {rpta}"
                    });
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = JsonConvert.SerializeObject(new
                {
                    MensajeError = $"Error aplazando la cuota | {ex.Message}"
                });
                return respuesta;
            }
        }
        public RespuestaServicioModel ReingresarCuota(CuotaMalaBindingModel cuota)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                string rpta =
                    this.IAgendamiento_cobrosDac.ReingresarCuota(cuota.Id_agendamiento).Result;

                if (rpta.Equals("OK"))
                {
                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(cuota);
                }
                else
                {
                    respuesta.Correcto = true;
                    respuesta.Respuesta = JsonConvert.SerializeObject(new
                    {
                        MensajeError = $"Error reingresando la cuota | {rpta}"
                    });
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = JsonConvert.SerializeObject(new
                {
                    MensajeError = $"Error reingresando la cuota | {ex.Message}"
                });
                return respuesta;
            }
        }
        #endregion
    }
}

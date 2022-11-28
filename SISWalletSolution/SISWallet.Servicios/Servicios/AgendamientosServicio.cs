using Newtonsoft.Json;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;
using System.Data;

namespace SISWallet.Servicios.Servicios
{
    public class AgendamientosServicio : IAgendamientosServicio
    {
        #region CONSTRUCTOR
        public IAgendamiento_cobrosDac IAgendamiento_cobrosDac { get; set; }
        public AgendamientosServicio(IAgendamiento_cobrosDac IAgendamiento_cobrosDac)
        {
            this.IAgendamiento_cobrosDac = IAgendamiento_cobrosDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel BuscarAgendamientos(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.IAgendamiento_cobrosDac.BuscarAgendamiento(busqueda.Tipo_busqueda, 
                    busqueda.Textos_busqueda.ToArray()).Result;

                string rpta = result.rpta;

                DataTable dtAgendamientos = result.dtAgendamientos;

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
        #endregion
    }
}

namespace SISWallet.AccesoDatos
{
    using SISWallet.Entidades.Models;
    using SISWallet.AccesoDatos.Interfaces;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using SISWallet.Entidades.ModelosBindeo;

    public class DSolicitudes : ISolicitudesDac
    {
        #region MENSAJE
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.Mensaje_respuesta = e.Message;
        }
        #endregion

        #region VARIABLES
        string _mensaje_respuesta;
        public string Mensaje_respuesta
        {
            get
            {
                return _mensaje_respuesta;
            }

            set
            {
                _mensaje_respuesta = value;
            }
        }
        #endregion

        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DSolicitudes(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region METODO INSERTAR
        public Task<string> InsertarSolicitud(Solicitudes solicitud)
        {
            string rpta = string.Empty;

            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Solicitudes_i",
                    CommandType = CommandType.StoredProcedure,
                };

                SqlParameter Id_solicitud = new()
                {
                    ParameterName = "@Id_solicitud",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_solicitud);

                SqlParameter Id_tipo_solicitud = new()
                {
                    ParameterName = "@Id_tipo_solicitud",
                    SqlDbType = SqlDbType.Int,
                    Value = solicitud.Id_tipo_solicitud
                };
                SqlCmd.Parameters.Add(Id_tipo_solicitud);

                SqlParameter Id_usuario = new ()
                {
                    ParameterName = "@Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Value = solicitud.Id_usuario
                };
                SqlCmd.Parameters.Add(Id_usuario);

                SqlParameter Parametro_solicitud = new()
                {
                    ParameterName = "@Parametro_solicitud",
                    SqlDbType = SqlDbType.VarChar,
                    Value = solicitud.Parametro_solicitud.Trim()
                };
                SqlCmd.Parameters.Add(Parametro_solicitud);

                SqlParameter Fecha_solicitud = new ()
                {
                    ParameterName = "@Fecha_solicitud",
                    SqlDbType = SqlDbType.Date,
                    Value = solicitud.Fecha_solicitud,
                };
                SqlCmd.Parameters.Add(Fecha_solicitud);

                SqlParameter Hora_solicitud = new ()
                {
                    ParameterName = "@Hora_solicitud",
                    SqlDbType = SqlDbType.Time,
                    Size = 2,
                    Value = solicitud.Hora_solicitud,
                };
                SqlCmd.Parameters.Add(Hora_solicitud);

                SqlParameter Asunto_solicitud = new ()
                {
                    ParameterName = "@Asunto_solicitud",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = solicitud.Asunto_solicitud.Trim()
                };
                SqlCmd.Parameters.Add(Asunto_solicitud);

                SqlParameter Descripcion_solicitud = new()
                {
                    ParameterName = "@Descripcion_solicitud",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 500,
                    Value = solicitud.Descripcion_solicitud.Trim()
                };
                SqlCmd.Parameters.Add(Descripcion_solicitud);

                SqlParameter Estado_solicitud = new ()
                {
                    ParameterName = "@Estado_solicitud",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = solicitud.Estado_solicitud.Trim()
                };
                SqlCmd.Parameters.Add(Estado_solicitud);

                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO SE INGRESÓ";
                if (!rpta.Equals("OK"))
                {
                    if (this.Mensaje_respuesta != null)
                    {
                        rpta = this.Mensaje_respuesta;
                    }
                }
                else
                    solicitud.Id_solicitud = Convert.ToInt32(SqlCmd.Parameters["@Id_solicitud"].Value);                
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult(rpta);
        }
        #endregion

        #region METODO BUSCAR SOLICITUDES
        public Task<(DataTable dt, string rpta)> BuscarSolicitudes(BusquedaBindingModel busqueda)
        {
            string rpta = "OK";

            DataTable DtResultado = new("Solicitudes");
            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                SqlCommand Sqlcmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Solicitudes_g",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "@Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Tipo_busqueda.Trim()
                };
                Sqlcmd.Parameters.Add(Tipo_busqueda);

                SqlParameter Texto_busqueda1 = new()
                {
                    ParameterName = "@Texto_busqueda1",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Texto_busqueda1.Trim()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda1);

                SqlParameter Texto_busqueda2 = new()
                {
                    ParameterName = "@Texto_busqueda2",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Texto_busqueda2 ?? ""
                };
                Sqlcmd.Parameters.Add(Texto_busqueda2);

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(DtResultado);

                if (DtResultado.Rows.Count < 1)
                    DtResultado = null;               
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                DtResultado = null;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                DtResultado = null;
            }
            return Task.FromResult((DtResultado, rpta));
        }
        #endregion

        #region METODO BUSCAR TIPO SOLICITUDES
        public Task<(DataTable dt, string rpta)> BuscarTipoSolicitudes(BusquedaBindingModel busqueda)
        {
            string rpta = "OK";

            DataTable DtResultado = new("TipoSolicitudes");
            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                SqlCommand Sqlcmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Tipo_solicitudes_g",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "@Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Tipo_busqueda.Trim()
                };
                Sqlcmd.Parameters.Add(Tipo_busqueda);

                SqlParameter Texto_busqueda = new()
                {
                    ParameterName = "@Texto_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Texto_busqueda1.Trim()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda);

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(DtResultado);

                if (DtResultado.Rows.Count < 1)
                    DtResultado = null;
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                DtResultado = null;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                DtResultado = null;
            }
            return Task.FromResult((DtResultado, rpta));
        }
        #endregion
    }
}

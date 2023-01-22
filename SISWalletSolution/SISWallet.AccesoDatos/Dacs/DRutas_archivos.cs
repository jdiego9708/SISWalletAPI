namespace SISWallet.AccesoDatos
{
    using SISWallet.Entidades.Models;
    using SISWallet.AccesoDatos.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq;
    using SISWallet.Entidades.ModelosBindeo;
    using SISWallet.Entidades.Helpers;
    using SISWallet.Entidades.Modelos;

    public class DRutas_archivos : IRutas_archivosDac
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
        public DRutas_archivos(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region METODO INSERTAR RUTA
        public Task<string> InsertarRuta(Rutas_archivos ruta)
        {
            int contador = 0;
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
                    CommandText = "sp_Rutas_archivos_i",
                    CommandType = CommandType.StoredProcedure
                };

                #region VARIABLES
                SqlParameter Id_ruta_archivo = new()
                {
                    ParameterName = "@Id_ruta_archivo",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_ruta_archivo);

                SqlParameter Id_usuario = new()
                {
                    ParameterName = "@Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Value = ruta.Id_usuario
                };
                SqlCmd.Parameters.Add(Id_usuario);

                SqlParameter Tipo_archivo = new()
                {
                    ParameterName = "@Tipo_archivo",
                    SqlDbType = SqlDbType.VarChar,
                    Value = ruta.Tipo_archivo   
                };
                SqlCmd.Parameters.Add(Tipo_archivo);

                SqlParameter Fecha_archivo = new()
                {
                    ParameterName = "@Fecha_archivo",
                    SqlDbType = SqlDbType.Date,
                    Value = ruta.Fecha_archivo,
                };
                SqlCmd.Parameters.Add(Fecha_archivo);
                contador += 1;

                SqlParameter Hora_archivo = new()
                {
                    ParameterName = "@Hora_archivo",
                    SqlDbType = SqlDbType.Time,
                    Value = ruta.Hora_archivo,
                };
                SqlCmd.Parameters.Add(Hora_archivo);
                contador += 1;

                SqlParameter Ruta_archivo = new()
                {
                    ParameterName = "@Ruta_archivo",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 8000,
                    Value = ruta.Ruta_archivo.Trim(),
                };
                SqlCmd.Parameters.Add(Ruta_archivo);
                contador += 1;

                SqlParameter Nombre_archivo = new()
                {
                    ParameterName = "@Nombre_archivo",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 500,
                    Value = ruta.Nombre_archivo.Trim(),
                };
                SqlCmd.Parameters.Add(Nombre_archivo);
                contador += 1;

                SqlParameter Extension_archivo = new()
                {
                    ParameterName = "@Extension_archivo",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = ruta.Extension_archivo.Trim(),
                };
                SqlCmd.Parameters.Add(Extension_archivo);
                contador += 1;
               
                #endregion

                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO SE INGRESÓ";

                if (!rpta.Equals("OK"))
                {
                    if (this.Mensaje_respuesta != null)
                    {
                        rpta = this.Mensaje_respuesta;
                    }
                }
                else
                {
                    ruta.Id_ruta_archivo = Convert.ToInt32(SqlCmd.Parameters["@Id_ruta_archivo"].Value);
                }
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

        #region METODO BUSCAR RUTAS
        public Task<(DataTable dtRutas, string rpta)> BuscarRutas(string tipo_busqueda, string texto_busqueda)
        {
            string rpta = "OK";

            DataTable DtResultado = new("Rutas");
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
                    CommandText = "sp_Rutas_g",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "@Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = tipo_busqueda.Trim()
                };
                Sqlcmd.Parameters.Add(Tipo_busqueda);

                SqlParameter Texto_busqueda = new()
                {
                    ParameterName = "@Texto_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = texto_busqueda.Trim()
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
